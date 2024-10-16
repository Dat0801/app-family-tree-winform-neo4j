using DTO;
using Neo4j.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                node["occupation"]?.As<string>()
            );
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
