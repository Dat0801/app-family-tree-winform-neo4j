using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PersonRelationship
    {
        public Person Person { get; set; }
        public string Relationship { get; set; }
        public Person RelatedPerson { get; set; }

        public PersonRelationship(Person person, string relationship, Person reletedperson)
        {
            Person = person;
            Relationship = relationship;
            RelatedPerson = reletedperson;
        }
    }
}
