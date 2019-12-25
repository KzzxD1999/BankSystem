using System;

namespace BankSystem.BL.Models
{
    [Serializable]
    public class Card
    {
        public string CardNumber { get; set; }
        public string UserName { get; set; }
        public bool IsBlocked { get; set; } = false;

        
    }
}