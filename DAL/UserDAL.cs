using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4j.Driver;

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
    }
}
