using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using Neo4j.Driver;

namespace DAL
{
    public class PersonDAL
    {
        private readonly IDriver _driver;

        public PersonDAL()
        {
            // Kết nối đến cơ sở dữ liệu Neo4j
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
                node["occupation"]?.As<string>(),
                node["biography"]?.As<string>()
            );
        }

        // Lấy thông tin thành viên và các quan hệ
        public async Task<List<PersonRelationship>> GetFamilyTree(string name)
        {
            var familyRelationships = new List<PersonRelationship>();


            // Truy vấn tìm thành viên và các quan hệ
            var query = @"MATCH (p:Person)-[r]->(related)
                  WHERE p.name = $name
                  RETURN p, related, type(r) as relationship";

            var session = _driver.AsyncSession();
            try
            {
                var result = await session.RunAsync(query, new { name });
                await result.ForEachAsync(record =>
                {
                    // Khởi tạo đối tượng cho người chính
                    var person = CreatePersonDTOFromNode(record["p"].As<INode>());

                    // Khởi tạo đối tượng cho thành viên liên quan
                    var relatedPerson = CreatePersonDTOFromNode(record["related"].As<INode>());

                    // Tạo đối tượng PersonRelationship và thêm vào danh sách
                    var relationship = record["relationship"].As<string>();
                    familyRelationships.Add(new PersonRelationship(
                        person,
                        relationship,
                        relatedPerson
                    ));
                });
            }
            finally
            {
                await session.CloseAsync();
            }

            return familyRelationships;
        }
    }
}
