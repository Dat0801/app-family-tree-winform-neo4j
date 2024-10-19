using DTO;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PersonDAL
    {
        private readonly IDriver _driver;

        public PersonDAL()
        {
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "08012003"));
        }
        private Person CreatePersonDTOFromNode(INode node)
        {
            return new Person(
                node["name"].As<string>(),
                node["date_of_birth"]?.As<string>(),
                node["gender"]?.As<string>(),
                node["address"]?.As<string>(),
                node["phone_number"]?.As<string>(),
                node["occupation"]?.As<string>()
            );
        }

        public async Task<Person> GetPersonsByName(string name)
        {
            Person person = new Person();
            string query = @"
        MATCH (p:Person {name:$name})
        RETURN p";

            IAsyncSession session = _driver.AsyncSession();
            try
            {
                IResultCursor result = await session.RunAsync(query, new { name });
                await result.ForEachAsync(record =>
                {
                    person = CreatePersonDTOFromNode(record["p"].As<INode>());
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return person;
        }


        public async Task<List<Person>> GetPersonsByUserName(string userName)
        {
            List<Person> persons = new List<Person>();
            string query = @"
        MATCH (u:User {username: $userName})-[:OWNS]->(p:Person)
        RETURN p";

            IAsyncSession session = _driver.AsyncSession();
            try
            {
                IResultCursor result = await session.RunAsync(query, new { userName });
                await result.ForEachAsync(record =>
                {
                    INode personNode = record["p"].As<INode>();
                    Person person = CreatePersonDTOFromNode(personNode);
                    persons.Add(person);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return persons;
        }

        public async Task<List<Person>> GetPersonsWithRelationshipsByUserName(string userName)
        {
            List<Person> persons = new List<Person>();
            string query = @"
        MATCH (u:User {username: $userName})-[:OWNS]->(p:Person)
        OPTIONAL MATCH (p)-[r]->(relatedPerson:Person)
        RETURN p, collect(type(r)) AS relationships, collect(relatedPerson) AS relatedPersons";

            IAsyncSession session = _driver.AsyncSession();
            try
            {
                IResultCursor result = await session.RunAsync(query, new { userName });
                await result.ForEachAsync(record =>
                {
                    // Lấy node Person
                    INode personNode = record["p"].As<INode>();
                    Person person = CreatePersonDTOFromNode(personNode);

                    // Lấy các mối quan hệ
                    var relationships = record["relationships"].As<List<string>>();
                    var relatedPersonsNodes = record["relatedPersons"].As<List<INode>>();

                    // Chuyển đổi các node liên quan thành Person
                    List<PersonRelationship> personRelationships = new List<PersonRelationship>();

                    for (int i = 0; i < relatedPersonsNodes.Count; i++)
                    {
                        Person relatedPerson = CreatePersonDTOFromNode(relatedPersonsNodes[i]);
                        string relationshipType = relationships[i];

                        // Thêm các quan hệ vào danh sách
                        string displayRelationship;
                        if (relationshipType == "PARENT_OF")
                        {
                            displayRelationship = "Cha/Mẹ";
                        }
                        else if (relationshipType == "MARRIED_TO")
                        {
                            displayRelationship = "Vợ/Chồng";
                        }
                        else
                        {
                            displayRelationship = relationshipType;  // Nếu không khớp, giữ nguyên
                        }

                        personRelationships.Add(new PersonRelationship(person, displayRelationship, relatedPerson));
                    }

                    // Gắn các mối quan hệ vào đối tượng Person
                    foreach (var personRelationship in personRelationships)
                    {
                        person.RelatedPerson = personRelationship.RelatedPerson;
                        person.Relationship = personRelationship.Relationship;
                    }

                    // Thêm người này vào danh sách
                    persons.Add(person);
                });
            }
            finally
            {
                await session.CloseAsync();
            }

            return persons;
        }


        public async Task<List<Person>> SearchPersons(string fullname, DateTime? birthDate, string gender, string address, string phoneNumber, string occupation)
        {
            List<Person> persons = new List<Person>();
            StringBuilder query = new StringBuilder();
            string birthDateString = birthDate.HasValue ? birthDate.Value.ToString("yyyy-MM-dd") : null;
            query.Append("MATCH (u:User {id: $username})-[:OWNS]->(p:Person) WHERE 1=1 ");
            if (!string.IsNullOrEmpty(fullname))
            {
                query.Append("AND p.name CONTAINS $fullname ");
            }
            if (!string.IsNullOrEmpty(birthDateString))
            {
                query.Append("AND p.date_of_birth = $birthDate ");
            }
            if (!string.IsNullOrEmpty(gender))
            {
                query.Append("AND p.gender = $gender ");
            }
            if (!string.IsNullOrEmpty(address))
            {
                query.Append("AND p.address CONTAINS $address ");
            }
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                query.Append("AND p.phone_number CONTAINS $phoneNumber ");
            }
            if (!string.IsNullOrEmpty(occupation))
            {
                query.Append("AND p.occupation CONTAINS $occupation ");
            }
            query.Append("RETURN p");
            string username = UserContext.CurrentUserName;
            IAsyncSession session = _driver.AsyncSession();
            try
            {
                IResultCursor result = await session.RunAsync(query.ToString(), new { username, fullname, birthDate = birthDateString, gender, address, phoneNumber, occupation });

                await result.ForEachAsync(record =>
                {
                    INode personNode = record["p"].As<INode>();
                    Person person = CreatePersonDTOFromNode(personNode);
                    persons.Add(person);
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return persons;
        }

        public bool UpdatePerson(string fullname, DateTime? birthDate, string gender, string phoneNumber, string address, string occupation)
        {
            string query = @"
                MATCH (p:Person {name: $fullname})
                SET p.date_of_birth = $birthDate,
                    p.gender = $gender,
                    p.phone_number = $phoneNumber,
                    p.address = $address,
                    p.occupation = $occupation
                RETURN p";

            IAsyncSession session = _driver.AsyncSession();
            try
            {
                session.RunAsync(query, new
                {
                    fullname,
                    birthDate = birthDate.HasValue ? birthDate.Value.ToString("yyyy-MM-dd") : null,
                    gender,
                    phoneNumber,
                    address,
                    occupation
                }).Wait();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                session.CloseAsync().Wait();
            }
        }

        public async Task<bool> DeletePerson(string fullname, string phoneNumber)
        {
            IAsyncSession session = _driver.AsyncSession();
            try
            {
                string query = @"
                MATCH (p:Person {name: $fullname, phone_number: $phoneNumber})
                DETACH DELETE p";

                var result = await session.RunAsync(query, new { fullname, phoneNumber });
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<PersonRelationship>> GetPersonRelationships(string personName)
        {
            List<PersonRelationship> relationships = new List<PersonRelationship>();

            // Tạo session từ driver
            using (var session = _driver.AsyncSession())
            {
                // Truy vấn Neo4j để lấy quan hệ giữa người và thân nhân
                string query = @"
                    MATCH (p:Person {name: $personName})-[relationship]->(relatedPerson:Person)
                    RETURN p, relatedPerson, type(relationship) AS relationship";

                var result = await session.RunAsync(query, new { personName });

                // Xử lý kết quả trả về từ truy vấn
                await result.ForEachAsync(record =>
                {
                    Person person = CreatePersonDTOFromNode(record["p"].As<INode>());
                    Person relatedPerson = CreatePersonDTOFromNode(record["relatedPerson"].As<INode>());
                    string relationship = record["relationship"].As<string>();

                    // Thêm đối tượng PersonRelationship vào danh sách
                    relationships.Add(new PersonRelationship(person, relationship, relatedPerson));
                });
            }

            return relationships;
        }

        // Lấy thông tin thành viên và các quan hệ
        public async Task<List<PersonRelationship>> GetFamilyTree(string name, string username)
        {
            var familyRelationships = new List<PersonRelationship>();
            var processedChildren = new HashSet<long>(); // Để theo dõi các ID của con đã thêm

            // Truy vấn để lấy tất cả các thế hệ con của người chính (Bob)
            var query = @"
                        MATCH (u:User {username: $username})-[:OWNS]->(p:Person {name: $name})
                        OPTIONAL MATCH (p)-[:PARENT_OF*1..]->(descendants)
                        OPTIONAL MATCH (p)-[:MARRIED_TO]-(spouse)
                        RETURN p, collect(distinct descendants) AS descendants, spouse";

            var session = _driver.AsyncSession();
            try
            {
                var result = await session.RunAsync(query, new { username = username, name = name });
                var records = await result.ToListAsync(); // Lấy tất cả các bản ghi
                foreach (var record in records)
                {
                    // Khởi tạo đối tượng cho người chính (Bob)
                    var person = CreatePersonDTOFromNode(record["p"].As<INode>());
                    var spouseNode = record["spouse"] != null ? CreatePersonDTOFromNode(record["spouse"].As<INode>()) : null;

                    // Xử lý các mối quan hệ con
                    var descendants = record["descendants"].As<List<INode>>();
                    foreach (var childNode in descendants)
                    {
                        if (!processedChildren.Contains(childNode.Id)) // Kiểm tra nếu chưa xử lý
                        {
                            var child = CreatePersonDTOFromNode(childNode);
                            familyRelationships.Add(new PersonRelationship(person, "PARENT_OF", child));
                            processedChildren.Add(childNode.Id); // Đánh dấu đã thêm mối quan hệ

                            // Đệ quy để thêm mối quan hệ cho con của con
                            await AddDescendants(childNode, familyRelationships, child, processedChildren);
                        }
                    }

                    // Thêm mối quan hệ với vợ/chồng
                    if (spouseNode != null)
                    {
                        familyRelationships.Add(new PersonRelationship(person, "MARRIED_TO", spouseNode));
                    }
                }
            }
            finally
            {
                await session.CloseAsync();
            }

            return familyRelationships;
        }

        // Hàm đệ quy để thêm con của con
        private async Task AddDescendants(INode parentNode, List<PersonRelationship> familyRelationships, Person parentPerson, HashSet<long> processedChildren)
        {
            var query = @"
                        MATCH (p:Person)-[:PARENT_OF]->(descendants)
                        WHERE id(p) = $parentId
                        RETURN collect(distinct descendants) AS descendants";

            var session = _driver.AsyncSession();
            try
            {
                var result = await session.RunAsync(query, new { parentId = parentNode.Id });
                var records = await result.ToListAsync(); // Lấy tất cả các bản ghi
                foreach (var record in records)
                {
                    var childNodes = record["descendants"].As<List<INode>>();
                    foreach (var childNode in childNodes)
                    {
                        if (!processedChildren.Contains(childNode.Id)) // Kiểm tra nếu chưa xử lý
                        {
                            var child = CreatePersonDTOFromNode(childNode);
                            familyRelationships.Add(new PersonRelationship(parentPerson, "PARENT_OF", child));
                            processedChildren.Add(childNode.Id); // Đánh dấu đã thêm mối quan hệ

                            // Đệ quy cho con của con
                            await AddDescendants(childNode, familyRelationships, child, processedChildren); // Thêm await
                        }
                    }
                }
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<bool> AddPersonWithoutRelationship(Person person)
        {
            try
            {
                // Mở phiên làm việc với Neo4j
                var session = _driver.AsyncSession();

                // Tạo câu lệnh Cypher để thêm một nút Person
                string query = @"
            match (u:User{username:$username})
            CREATE (p:Person {
                name: $name, 
                date_of_birth: $dob, 
                gender: $gender, 
                address: $address, 
                phone_number: $phone, 
                occupation: $occupation
            }),
            (u)-[:OWNS]->(p)
            RETURN p";

                // Thực thi câu lệnh với tham số tương ứng từ đối tượng Person
                var result = await session.ExecuteWriteAsync(async tx =>
                {
                    var cursor = await tx.RunAsync(query, new
                    {
                        username = UserContext.CurrentUserName,
                        name = person.Name,
                        dob = person.DateOfBirth,
                        gender = person.Gender,
                        address = person.Address,
                        phone = person.PhoneNumber,
                        occupation = person.Occupation
                    });

                    return await cursor.SingleAsync();
                });

                // Đóng phiên làm việc
                await session.CloseAsync();

                return result != null;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddPersonWithParent(Person person, Person relatedPerson)
        {
            try
            {
                // Mở phiên làm việc với Neo4j
                var session = _driver.AsyncSession();

                // Tạo câu lệnh Cypher để thêm một nút Person và mối quan hệ
                string query = @"
                CREATE (p:Person {
                    name: $name, 
                    date_of_birth: $dob, 
                    gender: $gender, 
                    address: $address, 
                    phone_number: $phone, 
                    occupation: $occupation
                })
                WITH p
                MATCH (r:Person {name: $relatedName}),
                      (u:User {username: $username})
                CREATE (r)-[:PARENT_OF]->(p),
                       (u)-[:OWNS]->(p)
                RETURN p";

                // Thực thi câu lệnh với tham số tương ứng từ đối tượng Person và relatedPerson
                var result = await session.ExecuteWriteAsync(async tx =>
                {
                    var cursor = await tx.RunAsync(query, new
                    {
                        username = UserContext.CurrentUserName,
                        name = person.Name,
                        dob = person.DateOfBirth,
                        gender = person.Gender,
                        address = person.Address,
                        phone = person.PhoneNumber,
                        occupation = person.Occupation,
                        relatedName = relatedPerson.Name
                    });;

                    return await cursor.SingleAsync();
                });

                // Đóng phiên làm việc
                await session.CloseAsync();

                return result != null;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddPersonWithSpouse(Person person, Person relatedPerson)
        {
            try
            {
                // Mở phiên làm việc với Neo4j
                var session = _driver.AsyncSession();

                // Tạo câu lệnh Cypher để thêm một nút Person và mối quan hệ
                string query = @"
                CREATE (p:Person {
                    name: $name, 
                    date_of_birth: $dob, 
                    gender: $gender, 
                    address: $address, 
                    phone_number: $phone, 
                    occupation: $occupation
                })
                WITH p
                MATCH (r:Person {name: $relatedName}),
                      (u:User {username: $username})
                CREATE (r)-[:MARRIED_TO]->(p),
                       (u)-[:OWNS]->(p)
                RETURN p";

                // Thực thi câu lệnh với tham số tương ứng từ đối tượng Person và relatedPerson
                var result = await session.ExecuteWriteAsync(async tx =>
                {
                    var cursor = await tx.RunAsync(query, new
                    {
                        username = UserContext.CurrentUserName,
                        name = person.Name,
                        dob = person.DateOfBirth,
                        gender = person.Gender,
                        address = person.Address,
                        phone = person.PhoneNumber,
                        occupation = person.Occupation,
                        relatedName = relatedPerson.Name
                    }); ;

                    return await cursor.SingleAsync();
                });

                // Đóng phiên làm việc
                await session.CloseAsync();

                return result != null;
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu có
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

    }
}
