using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        // Phương thức khởi tạo
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
