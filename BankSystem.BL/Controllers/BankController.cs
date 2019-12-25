using BankSystem.BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankSystem.BL.Controllers
{
    public class BankController : BaseController
    {
        public User CurrentUser { get; set; }
        public List<User> Users { get; set; }
        public UserContoller UserContoller { get; set; }

        public List<Opertation> Opertations { get; set; }

        public const string FILE_NAME = "operation.dat";
        public BankController(UserContoller userContoller)
        {
            Users = userContoller.Users;
            CurrentUser = userContoller.CurrentUser;
            UserContoller = userContoller;
         
        }

        public void AddMoney(int moneyToAdd)
        {
            CurrentUser.Money += moneyToAdd;
            MessagesToSend("The money has been added");
            UserContoller.Save();
        }
        public void SaveOperation()
        {
            Save(FILE_NAME, CurrentUser.Opertation);
        }
        public void AddMoney(int moneyToAdd, string cardNumber)
        {
            User user = Users.FirstOrDefault(x => x.Card.CardNumber == cardNumber);
            
            if (user != null)
            {
                if (CurrentUser.Money >= moneyToAdd)
                {
                    double withoutP = moneyToAdd - (moneyToAdd * 0.01);
                    CurrentUser.Money -= moneyToAdd;
                    user.Money += moneyToAdd;
                    Opertation opertationSend = new Opertation(CurrentUser.Name, CurrentUser.Surname, "Send money", DateTime.Now, cardNumber, withoutP);
                    Opertation opertationGet = new Opertation(user.Name, user.Surname, "Get money", DateTime.Now, user.Card.CardNumber, withoutP);
                    user.Opertation.Add(opertationGet);
                    
                    CurrentUser.Opertation.Add(opertationSend);
                    UserContoller.Save();
                    SaveOperation();
                    
                    MessagesToSend($"Has been sent {moneyToAdd} money");
                    
                }
                else
                {
                    MessagesToSend("Money has been sent");
                }

            }
            else
            {
                MessagesToSend("Card number not found");
            }
        }
    }
}
