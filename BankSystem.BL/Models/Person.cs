using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.BL.Models
{

   //TODO: додати різні банки для того щоб була комісія
    [Serializable]
    public abstract class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }

        public string Login { get; set; }
        public Role Role { get; set; }
        public string FullName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
        public Person(string name, string surname, int age, string password, string login)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (string.IsNullOrEmpty(surname))
            {
                throw new ArgumentException("message", nameof(surname));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("message", nameof(password));
            }

            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ArgumentException("message", nameof(login));
            }

            Name = name;
            Surname = surname;
            Age = age;
            Password = password;
            Login = login;
        }
    }
}
