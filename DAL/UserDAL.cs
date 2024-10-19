using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4j.Driver;
using DTO;
namespace DAL
{
    public class UserDAL
    {
        private readonly IDriver _driver;

        public UserDAL()
        {
            _driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "08012003"));
        }

        public async Task<string> GetUserIdAsync(string username, string password)
        {
            password = UserContext.ComputeMD5Hash(password);
            var query = @"MATCH (u:User {username: $username, password: $password}) RETURN u.username AS username";

            var session = _driver.AsyncSession();
            try
            {
                var result = await session.RunAsync(query, new { username, password });
                if (await result.FetchAsync())
                {
                    return result.Current["username"].As<string>();
                }
            }
            finally
            {
                await session.CloseAsync();
            }

            return null;
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            var session = _driver.AsyncSession();
            try
            {
                // Kiểm tra xem người dùng đã tồn tại chưa
                var checkQuery = @"MATCH (u:User {username: $username}) RETURN u";
                var checkResult = await session.RunAsync(checkQuery, new { username });
                if (await checkResult.FetchAsync())
                {
                    return false;
                }
                // Hash mật khẩu trước khi lưu
                string hashedPassword = UserContext.ComputeMD5Hash(password);

                // Tạo tài khoản mới
                var createQuery = @"CREATE (u:User {username: $username, password: $hashedPassword}) RETURN u.username AS username";
                var createResult = await session.RunAsync(createQuery, new { username, hashedPassword });

                if (await createResult.FetchAsync())
                {
                    return true;
                }

                return false;
            }
            finally
            {
                await session.CloseAsync();
            }
        }
    }
}
