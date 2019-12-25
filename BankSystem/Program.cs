
using System;
using System.Linq;
using BankSystem.BL;
using BankSystem.BL.Controllers;

namespace BankSystem
{
    class Program
    {
         
        static void Main(string[] args)
        {
            UserContoller userContoller1 = new UserContoller();
            while (true)
            {
                Console.WriteLine("1)Login");
                Console.WriteLine("2)Registration");

                ConsoleKeyInfo key = Console.ReadKey();
                //Створити нормальний вхід!
                Menu(key);
            }
        }

        private static void Menu(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    Login();
                    break;
                case ConsoleKey.D2:
                    Registration();
                    break;
                default:
                    break;
            }
        }

        private static void Login()
        {

            UserContoller userContoller = new UserContoller();
            Console.Write("Enter login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();
   
            userContoller.Messages += UserContoller_Message;
            userContoller.Login(login, password);
            
            if (userContoller.IsNew)
            {
                Auth();
            }
            MainMenu(userContoller);
        }

        private static void Auth(string login = null, string password=null)
        {
            
            Console.WriteLine("1)Yes");
            Console.WriteLine("2)No (exit)");
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    Registration(login, password);
                    break;
                case ConsoleKey.D2:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        private static void Registration(string login=null, string password=null)
        {
            UserContoller userContoller = new UserContoller();
            if (!userContoller.IsError)
            {
                if (login == null)
                {
                    Console.WriteLine("Set login");
                    login = Console.ReadLine();
                }
                if (password == null)
                {
                    Console.WriteLine("Set Password");
                    password = Console.ReadLine();
                }
                Console.WriteLine("Set name");
                string name = Console.ReadLine();
                Console.WriteLine("Set surname");
                string surname = Console.ReadLine();
                Console.WriteLine("Set email");
                string email = Console.ReadLine();
                Console.WriteLine("Set age");
                int age = int.Parse(Console.ReadLine());
                Console.WriteLine("Set passport");
                string passport = Console.ReadLine();
                userContoller.Messages += UserContoller_Message;
                userContoller.CreateUser(name, surname, email, age, passport, password, login);
                if (userContoller.IsSuccess)
                {
                    MainMenu(userContoller);
                }
              
            }
            if (userContoller.IsError)
            {
                userContoller.Messages += UserContoller_Message;
                Console.WriteLine("Do you want to create an account with another login?");
                Auth();
            }
        }
        //Як діставати інформацію
        private static void MainMenu(UserContoller userContoller)
        {
            while (true)
            {
                Console.WriteLine("1) Inforamtion");
                Console.WriteLine("2) Add money");
                Console.WriteLine("3) Settings");
                Console.WriteLine("4) Exit");
                ConsoleKeyInfo key = Console.ReadKey();
                
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        InformationAboutUser(userContoller);
                        break;
                    case ConsoleKey.D2:
                        AddMoneyMenu(userContoller);
                        break;
                    case ConsoleKey.D3:
                        SettingsMenu(userContoller);
                        break;
                    case ConsoleKey.D4:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }

        
        }

        private static void SettingsMenu(UserContoller userContoller)
        {
            while (true)
            {
                Console.WriteLine("1)Change password");
                Console.WriteLine("2)Change name");
                Console.WriteLine("3)Change surname");
                Console.WriteLine("4)Change block own card");
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        Console.WriteLine("Set your new password");
                        string newPassword = Console.ReadLine();
                        userContoller.ChangePasswrod(newPassword);
                        break;
                    case ConsoleKey.D2:
                        break;
                    case ConsoleKey.D3:
                        break;
                    case ConsoleKey.D4:
                        break;
                }
            }
        }

        private static void AddMoneyMenu(UserContoller userContoller)
        {
            Console.WriteLine("Choose an option: ");
            Console.WriteLine("1) Add money to myself account: ");
            Console.WriteLine("2) Add money to another account: ");
            BankController bankController = new BankController(userContoller);
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    Console.WriteLine("How much money do you want to add?");
                    int moneyToAdd = int.Parse(Console.ReadLine());
                    bankController.AddMoney(moneyToAdd);
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("How much money do you want to send?");
                    Console.WriteLine("Комісія 1% при переведенні на інші банки");
                    int moneyToAdd1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Write card number");
                    string cardNumber = Console.ReadLine();
                    bankController.AddMoney(moneyToAdd1, cardNumber.Replace(" ", string.Empty));
                    break;
            }

        }

        private static void InformationAboutUser(UserContoller userContoller)
        {


            //var res = userContoller.CurrentUser.CardNumber.ToCharArray();
            var res = userContoller.CurrentUser.Card.CardNumber.ToCharArray();
            string str1 = "", str2="", str3="", str4 = "";
            for (int i = 0; i < res.Length; i++)
            {
                //Зробити через ink
                if (str1.Length < 4)
                {
                    str1 += res[i];
                }
                else if(str1.Length >= 4 && str2.Length < 4)
                {
                    str2 += res[i];
                }
                else if (str2.Length >= 4 && str3.Length < 4)
                {
                    str3 += res[i];
                }
                else if (str3.Length >= 4 && str4.Length < 4)
                {
                    str4 += res[i];
                }

            }
            Console.WriteLine($"User Name and Surname: {userContoller.CurrentUser.FullName}");
            Console.WriteLine($"Card number: {str1} {str2} {str3} {str4}");
            Console.WriteLine($"Money: {userContoller.CurrentUser.Money}");
            if (userContoller.CurrentUser.Opertation !=null)
            {
                foreach (var item in userContoller.CurrentUser.Opertation)
                {
                    Console.WriteLine($"{item.Name}: {item.Money} ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"From: {item.CardNumber}");

                    Console.WriteLine($"Name and Surname: {item.FullName}");
                    Console.ForegroundColor = ConsoleColor.White;


                }
            }
  
        }

        private static void UserContoller_Message(string obj)
        {
            Console.WriteLine(obj);
        }
    }
}
