using System;
using System.Collections.Generic;

namespace BankSystem.BL.Models
{
    [Serializable]
    public class Card
    {
        public string CardNumber { get; set; }
        public string UserName { get; set; }
        public bool IsBlocked { get; set; } = false;
        public string BankSystem { get; set; }
        
        public Bank Bank { get; set; }

        private Dictionary<int, string> System = new Dictionary<int, string>()
        {
            {1, "Visa" },
            {2, "MasterCard" }
        };
        public Card()
        {

        }
        public Card(int id)
        {
            BankSystem = System[id];
        }
        
    }
}