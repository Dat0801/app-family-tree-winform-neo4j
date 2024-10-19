using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class UserBLL
    {
        private readonly UserDAL _userDal;

        public UserBLL()
        {
            _userDal = new UserDAL();
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            // Gọi DAL để lấy ID người dùng
            return await _userDal.GetUserIdAsync(username, password);
        }

        public async Task<bool> RegisterUserAsync(string username, string password)
        {
            return await _userDal.RegisterUserAsync(username, password);
        }
    }
}
