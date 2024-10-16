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
        public async Task<List<Person>> GetPersonsByUserId(string userId)
        {
            return await personDAL.GetPersonsByUserId(userId);
        }
        public Task<List<PersonRelationship>> GetFamilyTree(string name, string userId)
        {
            return personDAL.GetFamilyTree(name, userId);
        }
        public async Task<List<Person>> SearchPersons(string fullname, DateTime? birthDate, string gender, string address, string phoneNumber, string occupation)
        {
            List<Person> persons = await personDAL.SearchPersons(fullname, birthDate, gender, address, phoneNumber, occupation);
            return persons;
        }
        public bool UpdatePerson(string fullname, DateTime? birthDate, string gender, string phoneNumber, string address, string occupation)
        {
            return personDAL.UpdatePerson(fullname, birthDate, gender, phoneNumber, address, occupation);
        }
        public Task<bool> DeletePerson(string fullname, string phoneNumber)
        {
            return personDAL.DeletePerson(fullname, phoneNumber);
        }
        public async Task<List<PersonRelationship>> GetPersonRelationships(string personName)
        {
            return await personDAL.GetPersonRelationships(personName);
        }

    }
}
