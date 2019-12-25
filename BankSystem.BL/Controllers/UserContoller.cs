using BankSystem.BL.Controllers;
using BankSystem.BL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace BankSystem.BL
{
    public class UserContoller : BaseController
    {
        public User CurrentUser { get; set; }
        public List<User> Users { get; set; }

        public bool IsNew { get; set; } = false;
        public bool IsError { get; set; } = false;
        public bool IsSuccess { get; set; }

        public const string FILE_NAME = "users.dat";

   
        public UserContoller()
        {
            Users = GetUser();
            User admin = Users.FirstOrDefault(x => x.Role.Name == "Banker");
            if (Users.Count() == 0 && admin == null)
            {
                User user = new User("Admin", "Admin", 99, "FDA688", "sdsd@mg.com", "1834asd", "KzzxD");
                user.Card = new Card();
                user.Card.CardNumber = GenerateCardCode();



                user.Role = new Role(2);
                Users.Add(user);
                Save();
            }
            
          
        }




        public User CreateUser(string name, string surname, string email, int age, string passport, string password,string login)
        {
            var check = Users.FirstOrDefault(x=>x.Login == login);
            if (check ==null)
            {

                while (true)
                {

                    string cardNumber = GenerateCardCode();



                    User usersCardNumber = new User
                    {
                        Card = new Card()
                    };
                    usersCardNumber = Users.FirstOrDefault(x => x.Card.CardNumber == cardNumber);
                    if (usersCardNumber == null)
                    {
                        User user = new User(name, surname, age, passport, email, password, login)
                        {
                            Role = new Role(2),
                            Opertation = new List<Opertation>(),
                            Card = new Card { CardNumber = cardNumber, UserName = email, IsBlocked = false},
                        };

                        Users.Add(user);
                        Save();
                        CurrentUser = user;
                        Console.WriteLine(user);
                        MessagesToSend("Successfully created");
                        IsSuccess = true;
                        return CurrentUser;
                        
                    }
                    else
                    {
                        GenerateCardCode();
                    }
                }
            }
            else
            {
                MessagesToSend("An account with the same login already exists.");
                IsError = true;
                return new User();
            }
            
        }


        private List<User> GetUser()
        {
            return Load<List<User>>(FILE_NAME) ?? new List<User>();
        } 


        public void Save()
        {
            Save(FILE_NAME, Users);
        }

        public void Login(string login, string password)
        {
            User user = Users.FirstOrDefault(x => x.Login.ToLower() == login.ToLower());
            if (user !=null && user.Password == password)
            {
                CurrentUser = user;
                CurrentUser.Opertation = user.Opertation.OrderByDescending(x => x.Date).ToList();
                string message = "You are logged in";
                MessagesToSend(message);
            }
            else
            {
                string message = "Account not found, do you want to create an account?";
                MessagesToSend(message);
                IsNew = true;
            }
        }

        private string GenerateCardCode()
        {
            var chars = Enumerable.Repeat("0123456789", 16);
            var random = new string(chars.SelectMany(x => x).OrderBy(c => Guid.NewGuid()).Take(16).ToArray());
            return random;
        }

        public void ChangePasswrod(string newPassword)
        {
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                CurrentUser.Password = newPassword;
                Save();
                string message = "You have updated your password!";
                MessagesToSend(message);
            }
            else
            {
                string message = "New password can't be null";
                MessagesToSend(message);

            }
        }
    }
}
