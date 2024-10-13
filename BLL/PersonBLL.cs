using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class PersonBLL
    {
        private readonly PersonDAL personDAL;

        public PersonBLL()
        {
            personDAL = new PersonDAL();
        }

        // Lấy thông tin gia đình của thành viên
        public Task<List<PersonRelationship>> GetFamilyTree(string name)
        {
            return personDAL.GetFamilyTree(name);
        }
    }
}
