using System;
using System.Collections.Generic;

namespace BankSystem.BL.Models
{
    [Serializable]
    public class Bank
    {

        public Dictionary<int, string> banks = new Dictionary<int, string>()
        {
            {1, "PrivatBank" },
            {2, "OshchadBank" },
            {3, "KredoBank" },
            {4, "IdeaBank" }
        };
        public string Name { get; set; }

        public Bank()
        {

        }
        public Bank(int bankId)
        {
            Name = banks[bankId];
        }

    }
}