using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Phương thức khởi tạo
        public User(string id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
    }
}
