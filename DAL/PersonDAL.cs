using DTO;
using Neo4j.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

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

        public async Task<List<Person>> GetPersonsByUserId(string userId)
        {
            List<Person> persons = new List<Person>();
            string query = @"
        MATCH (u:User {id: $userId})-[:OWNS]->(p:Person)
        RETURN p";

            IAsyncSession session = _driver.AsyncSession();
            try
            {
                IResultCursor result = await session.RunAsync(query, new { userId });
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

        public async Task<List<Person>> SearchPersons(string fullname, DateTime? birthDate, string gender, string address, string phoneNumber, string occupation)
        {
            List<Person> persons = new List<Person>();
            StringBuilder query = new StringBuilder();
            string birthDateString = birthDate.HasValue ? birthDate.Value.ToString("yyyy-MM-dd") : null;
            query.Append("MATCH (u:User {id: $userId})-[:OWNS]->(p:Person) WHERE 1=1 ");
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
            string userId = UserContext.CurrentUserId;
            IAsyncSession session = _driver.AsyncSession();
            try
            {
                IResultCursor result = await session.RunAsync(query.ToString(), new { userId, fullname, birthDate = birthDateString, gender, address, phoneNumber, occupation });

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
    }
}
