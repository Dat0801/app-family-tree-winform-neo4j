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
        public async Task<List<PersonRelationship>> GetFamilyTree(string name, string userId)
        {
            var familyRelationships = new List<PersonRelationship>();
            // Truy vấn tìm thành viên và các quan hệ
            var query = @"
                        MATCH (u:User {id: $userId})-[:OWNS]->(p:Person {name: $name})
                        OPTIONAL MATCH (p)-[:PARENT_OF]->(children)
                        OPTIONAL MATCH (p)-[:MARRIED_TO]-(spouse)
                        RETURN p, collect(children) AS children, spouse";

            var session = _driver.AsyncSession();
            try
            {
                var result = await session.RunAsync(query, new { userId = userId, name = name });
                await result.ForEachAsync(record =>
                {
                    // Khởi tạo đối tượng cho người chính
                    var person = CreatePersonDTOFromNode(record["p"].As<INode>());

                    // Xử lý các con cái (children)
                    var children = record["children"].As<List<INode>>();
                    foreach (var childNode in children)
                    {
                        var child = CreatePersonDTOFromNode(childNode);
                        familyRelationships.Add(new PersonRelationship(
                            person,
                            "PARENT_OF",
                            child
                        ));
                    }

                    // Xử lý vợ/chồng (spouse)
                    if (record["spouse"] != null && record["spouse"].As<INode>() != null)
                    {
                        var spouse = CreatePersonDTOFromNode(record["spouse"].As<INode>());
                        familyRelationships.Add(new PersonRelationship(
                            person,
                            "MARRIED_TO",
                            spouse
                        ));
                    }
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
