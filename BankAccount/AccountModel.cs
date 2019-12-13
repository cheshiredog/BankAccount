using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    class Account
    {
        public List<Card> Cards { get; set; } = new List<Card>();
        private string UserName { get; set; }
        private string UserSurname { get; set; }
        private int UserAge { get; set; }
        public string UserLogin
        {
            get
            {
                return UserName + UserSurname + Convert.ToString(UserAge);
            }
        }
        private string UserPass { get; set; }

        public Account(string userName, string userSurname, int userAge, string userPass, string cardName, string cardPin)
        {
            UserName = userName;
            UserSurname = userSurname;
            UserAge = userAge;
            UserPass = userPass;
            Cards.Add(new Card(cardName, cardPin));
        }

        public void AddNewCard(string cardName, string cardPin)
        {
            Cards.Add(new Card(cardName, cardPin));
        }

        public Card GetUserCard(string cardName, string cardPin)
        {
            Card userCard = Cards.FirstOrDefault(card => card.CardName == cardName && card.CardPin == cardPin);
            return userCard;
        }

        public string GetUserCardsNames()
        {
            string userCardsNames = "";

            foreach (Card card in Cards)
            {
                userCardsNames += card.CardName + " ";
            }

            return userCardsNames;
        }

        public void GetInfo(string userInputLogin, string userInputPass)
        {
            if (IsUserLogined(userInputLogin, userInputPass))
            {
                Console.WriteLine($"Name: {UserName}" +
                                  $"\nSurname: {UserSurname}" +
                                  $"\nAge: {UserAge}" +
                                  $"\nLogin: {UserLogin}" +
                                  $"\nPassword: {String.Concat(Enumerable.Repeat("*", UserPass.Length))}" +
                                  $"\nCards: {GetUserCardsNames()}");
            }
        }

        public bool IsUserLogined(string userInputLogin, string userInputPass)
        {
            if (userInputLogin != this.UserLogin && userInputPass != this.UserPass)
            {
                return false;
            }
            return true;
        }
    }

}
