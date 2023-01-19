using System;
using System.Collections.Generic;

namespace VendingMachine
{
    class VendingMachine
    {
        static void Main()
        {
            var coinsCatalog = new List<int>() { 1, 2, 5, 10, 20, 50 };
            Buy(LoadCoins(coinsCatalog));
            Console.WriteLine("Session ended");
        }

        static private Wallet LoadCoins(List<int> coinsCatalog)
        {
            var machineWallet = new Wallet();

            foreach (var value in coinsCatalog)
            {
                Console.WriteLine("How many {0} cent coins you would like to add", value);
                int amount = Convert.ToInt32(Console.ReadLine());
                machineWallet.AddCoins(new Coins(value, amount));
            }
            Console.Clear();
            return machineWallet;
        }

        static private void Buy(Wallet machineWallet)
        {
            Console.WriteLine("What is the price");
            int price = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Add coins");
            Console.WriteLine("Value:");
            int value = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Amount:");
            int amount = Convert.ToInt32(Console.ReadLine());

            var insertedCoins = new Wallet();
            insertedCoins.AddCoins(new Coins(value, amount));

            PayChange(price, machineWallet, insertedCoins);
        }

        /// <summary>
        /// At first machine is trying to return change from inserted coins (in case of too much coins are inserted), 
        /// then from vending maschine available coins.
        /// </summary>
        private static void PayChange(int price, Wallet machineWallet, Wallet insertedCoins)
        {
            int change = insertedCoins.Balance() - price;
            if (change < 0)
            {
                Console.WriteLine("Not enough coins");
            }
            else
            {
                var changeWallet = ChangeCalculator.Calculate(insertedCoins, change, out int remainingchange);
                machineWallet.Combine(insertedCoins);
                changeWallet.Combine(ChangeCalculator.Calculate(machineWallet, remainingchange, out remainingchange));

                if (remainingchange > 0)

                {
                    Console.WriteLine("Not enough coins for change");
                }
                else
                {
                    foreach (var returnCoins in changeWallet.Coins)
                    {
                        Console.WriteLine("Return coins");
                        Console.WriteLine("{0} of {1} cent", returnCoins.Amount, returnCoins.Value);
                    }
                }
            }
        }
    }
}
