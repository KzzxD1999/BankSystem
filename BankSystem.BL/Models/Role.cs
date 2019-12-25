using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.BL.Models
{
    [Serializable]
    public class Role
    {
        private Dictionary<int, string> roles = new Dictionary<int, string>()
        {
            {1, "User" },
            {2, "Banker" }
        };

        public string Name { get; set; }

        public Role(int id)
        {
            Name = roles[id];
        }

    }
}
