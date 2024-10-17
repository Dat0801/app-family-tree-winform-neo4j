using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BLL
{
    public class PersonBLL
    {
        private readonly PersonDAL personDAL;

        public PersonBLL()
        {
            personDAL = new PersonDAL();
        }

        public async Task<List<Person>> GetPersonsByUserName(string userName)
        {
            return await personDAL.GetPersonsByUserName(userName);
        }

        public Task<List<PersonRelationship>> GetFamilyTree(string name, string userName)
        {
            return personDAL.GetFamilyTree(name, userName);
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

        public async Task<Person> GetPersonsByName(string name)
        {
            return await personDAL.GetPersonsByName(name);
        }

        public async Task<List<Person>> GetPersonsWithRelationshipsByUserName(string userName)
        {
            return await personDAL.GetPersonsWithRelationshipsByUserName(userName);
        }

        public async Task<bool> AddPersonWithoutRelationship(Person person)
        {
            return await personDAL.AddPersonWithoutRelationship(person);
        }

        public async Task<bool> AddPersonWithRelationship(Person person, string relationshipType, Person relatedPerson)
        {
            return await personDAL.AddPersonWithRelationship(person, relationshipType, relatedPerson);
        }
    }
}
