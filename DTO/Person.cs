﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Person
    {
        public string STT { get; set; }
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Occupation { get; set; }
        public string DateOfDeath { get; set; }
        public string Relationship { get; set; }
        public Person RelatedPerson { get; set; }

        public Person(string name, string dateOfBirth, string gender, string address, string phoneNumber, string occupation)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            PhoneNumber = phoneNumber;
            Occupation = occupation;
        }

        public Person()
        {

        }
    }
}
