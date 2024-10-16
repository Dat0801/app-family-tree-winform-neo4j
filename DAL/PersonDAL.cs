using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Neo4j.Driver;
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
                node.Properties.ContainsKey("name") ? node["name"].As<string>() : null,
                node.Properties.ContainsKey("date_of_birth") ? node["date_of_birth"].As<string>() : null,
                node.Properties.ContainsKey("gender") ? node["gender"].As<string>() : null,
                node.Properties.ContainsKey("address") ? node["address"].As<string>() : null,
                node.Properties.ContainsKey("phone_number") ? node["phone_number"].As<string>() : null,
                node.Properties.ContainsKey("occupation") ? node["occupation"].As<string>() : null,
                node.Properties.ContainsKey("biography") ? node["biography"].As<string>() : null
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
        public async Task<List<PersonRelationship>> GetFamilyTree(string name, string userId)
        {
            List<PersonRelationship> familyRelationships = new List<PersonRelationship>();
            string query = @"
                        MATCH ((p:Person {name: $name})
                        OPTIONAL MATCH (p)-[:PARENT_OF]->(children)
                        OPTIONAL MATCH (p)-[:MARRIED_TO]-(spouse)
                        RETURN p, collect(children) AS children, spouse";

            IAsyncSession session = _driver.AsyncSession();
            try
            {
                IResultCursor result = await session.RunAsync(query, new { userId = userId, name = name });
                await result.ForEachAsync(record =>
                {
                    INode personNode = record["p"].As<INode>();
                    Person person = CreatePersonDTOFromNode(personNode);
                    List<INode> childrenNodes = record["children"].As<List<INode>>();
                    foreach (INode childNode in childrenNodes)
                    {
                        Person child = CreatePersonDTOFromNode(childNode);
                        familyRelationships.Add(new PersonRelationship(person, "PARENT_OF", child));
                    }
                    INode spouseNode = record["spouse"].As<INode>();
                    if (spouseNode != null)
                    {
                        Person spouse = CreatePersonDTOFromNode(spouseNode);
                        familyRelationships.Add(new PersonRelationship(person, "MARRIED_TO", spouse));
                    }
                });
            }
            finally
            {
                await session.CloseAsync();
            }
            return familyRelationships;
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




    }
}
