using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Entities.Entities
{
    public enum Gender { Male, Female }

    [Serializable]
    public class Person
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime DateOfBirth { get; protected set; }
        public Gender Gender { get; protected set; }
        public Person() { }
        public Person(string firstName, string lastName, DateTime dateOfBirth, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }
    }
}
