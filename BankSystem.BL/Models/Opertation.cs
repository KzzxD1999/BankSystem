using System;

namespace BankSystem.BL.Models
{
    [Serializable]
    public class Opertation
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string CardNumber { get; set; }
        public double Money { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public Opertation(string firstName, string lastName, string name, DateTime date, string cardNumber, double money)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new ArgumentException("message", nameof(firstName));
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                throw new ArgumentException("message", nameof(lastName));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("message", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new ArgumentException("message", nameof(cardNumber));
            }

            FirstName = firstName;
            LastName = lastName;
            Name = name;
            Date = date;
            CardNumber = cardNumber;
            Money = money;
        }
    }
}