using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    class Card
    {
        private int UserBalance { get; set; } = 0;
        private static int MinUserBalance { get; } = 0;
        public string CardName { get; set; }
        public string CardPin { get; set; }

        public Card(string cardName, string cardPin)
        {
            CardName = cardName;
            CardPin = cardPin;
        }

        public void Replenishe(int sum, string cardPin)
        {
            this.UserBalance += sum;
            Console.WriteLine($"Balance: {this.GetBalance()}");
        }

        public void Withdrow(int sum, string cardPin)
        {
        
            if (this.UserBalance - sum >= MinUserBalance)
            {
                this.UserBalance -= sum;
                Console.WriteLine($"Balance: {this.GetBalance()}");
            }

            else
            {
                Console.WriteLine("Invalid transaction.");
            }
        
        }

        public int GetBalance()
        {
            return this.UserBalance;
        }

        // In addition
        // Also you need to implement functionality of restoring pin of card if user forgot it (for first think about it and how we can implement it)
    }
}
