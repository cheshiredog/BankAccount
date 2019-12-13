using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Account> users = new List<Account>();

            while (true)
            {
                Console.Write("Sign in (1) or sign up (2)?\n");
                string userAnswer = Console.ReadLine();

                SignUpSignIn(userAnswer, users);
            }
        }

        static void SignUpSignIn(string userAnswer, List<Account> users)
        {
            switch (userAnswer)
            {
                case "2": // sign up
                    AddNewUser(AskUserName(), AskUserSurname(), AskUserAge(), AskUserPass(), users, AskUserCardName(), AskUserCardPin());
                    break;

                case "1": // sign in
                    string login = InputUserLogin();
                    string pass = InputUserPass();
                    Account acc = GetUserAccount(login, pass, users);

                    if (acc == null)
                    {
                        Console.WriteLine("Wrong login or password.");
                        break;
                    }

                    do
                    {
                        Console.Write("Create new card (1), change balance (2), get info (3) or sign out (4)?");
                        userAnswer = Console.ReadLine();
                        OperationWithCards(userAnswer, acc, login, pass);
                    }
                    while (userAnswer != "4");
                    break;

                default:
                    Console.WriteLine("Wrong answer.");
                    break;
            }
        }

        static void OperationWithCards(string userAnswer, Account acc, string userLogin, string userPass)
        {
            switch (userAnswer)
            {
                case "1":
                    Console.Write("Card name: ");
                    string inputCardName = Console.ReadLine();
                    Console.Write("Card pin: ");
                    string inputCardPin = Console.ReadLine();
                    acc.AddNewCard(inputCardName, inputCardPin);
                    break;

                case "2":
                    ChangeBalance(acc);
                    break;

                case "3":
                    acc.GetInfo(userLogin, userPass);
                    break;

                default:
                    break;
            }
        }

        static string AskUserName()
        {
            Console.Write("\nEnter user data.\n\nName: ");
            string userName = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Wrong name.");
                userName = AskUserName();
            }

            return userName;
        }

        static string AskUserSurname()
        {
            Console.Write("Surname: ");
            string userSurname = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(userSurname))
            {
                Console.WriteLine("Wrong surname.");
                userSurname = AskUserSurname();
            }

            return userSurname;
        }

        static int AskUserAge()
        {
            Console.Write("Age: ");
            int userAge = Convert.ToInt32(Console.ReadLine());

            if (userAge < 18)
            {
                Console.WriteLine("Age must be grater than 18.");
                userAge = AskUserAge();
            }

            return userAge;
        }

        static string AskUserPass()
        {
            Console.Write("Password: ");
            string userPass = Console.ReadLine();

            if (userPass.Length < 6)
            {
                Console.WriteLine("Password must be longer than 5 characters.");
                userPass = AskUserPass();
            }

            return userPass;
        }

        static string AskUserCardName()
        {
            Console.Write("Card name: ");
            string cardName = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(cardName))
            {
                Console.WriteLine("Wrong card name.");
                cardName = AskUserCardName();
            }

            return cardName;
        }

        static string AskUserCardPin()
        {
            Console.Write("Card pin: ");
            string cardPin = Console.ReadLine();

            if (cardPin.Length != 4)
            {
                Console.Write("Wrong card pin.");
                cardPin = AskUserCardPin();
            }

            return cardPin;
        }

        static void AddNewUser(string userName, string userSurname, int userAge, string userPass, List<Account> listOfUsers, string cardName, string cardPin)
        {
            Account newUser = new Account(userName, userSurname, userAge, userPass, cardName, cardPin);
            Console.WriteLine();
            newUser.GetInfo(newUser.UserLogin, userPass);
            listOfUsers.Add(newUser);
        }

        static string InputUserLogin()
        {
            Console.Write("\nEnter user login: ");
            string userLogin = Console.ReadLine();
            return userLogin;
        }

        static string InputUserPass()
        {
            Console.Write("Enter user password: ");
            string userInputPass = Console.ReadLine();
            return userInputPass;
        }

        static Account GetUserAccount(string userLogin, string userInputPass, List<Account> users)
        {
            Account acc = users.FirstOrDefault(user => user.IsUserLogined(userLogin, userInputPass));
            return acc;
        }

        static void ChangeBalance(Account user)
        {
            string signOut = "No";

            while (signOut == "No")
            {
                Console.Write("\nDo you want to replenishe (1) balance or withdraw (2) money? ");
                string userAnswer = Console.ReadLine();
                int sum;
                Console.Write($"Enter card name ({user.GetUserCardsNames()}): ");
                string cardName = Console.ReadLine();
                Console.Write("Enter card pin: ");
                string cardPin = Console.ReadLine();
                Card userCard = user.GetUserCard(cardName, cardPin);
                if (userCard == null)
                {
                    Console.WriteLine("Wrong card name or pin.");
                    continue;
                }

                switch (userAnswer)
                {
                    case "1":
                        Console.Write("Enter sum: ");
                        sum = Convert.ToInt32(Console.ReadLine());
                        userCard.Replenishe(sum, cardPin);
                        break;

                    case "2":
                        Console.Write("Enter sum: ");
                        sum = Convert.ToInt32(Console.ReadLine());
                        userCard.Withdrow(sum, cardPin);
                        break;

                    default:
                        Console.WriteLine("Wrong answer.");
                        break;
                }

                Console.WriteLine("Do you want to return to operations with account? (Yes or No) ");
                signOut = Console.ReadLine();
            }
        }
    }
}
