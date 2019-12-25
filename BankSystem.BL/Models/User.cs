using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.BL.Models
{
   
    [Serializable]
    public class User : Person
    {
        public string Email { get; set; }
        public double Money { get; set; } = 0;
        public string Passport { get; set; }

        //TODO: Створити окермйни клас для карточки з можливістю блоку.
        public Card Card { get; set; }
        public string CardNumber { get; set; }
        public List<Opertation> Opertation { get; set; }
        public User():base("0","0",5, "1", "R")
        {

        }
        public User(string name, string surname, int age, string passport, string email, string password, string login) : base(name, surname, age, password, login)
        {
            if (string.IsNullOrWhiteSpace(passport))
            {
                throw new ArgumentException("message", nameof(passport));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("message", nameof(email));
            }

            //if (string.IsNullOrWhiteSpace(cardNumber))
            //{
            //    throw new ArgumentException("message", nameof(cardNumber));
            //}

            Passport = passport;
            Email = email;
            //CardNumber = cardNumber;
        }
        public override string ToString()
        {
            return $"Name and Surname: {FullName}";
        }
    }
}
