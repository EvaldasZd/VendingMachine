using System.Collections.Generic;

namespace VendingMachine
{
    /// <summary>
    /// Wallet class contains collection of various coins.
    /// </summary>
    public class Wallet
    {
        public List<Coins> Coins { get; private set; } = new List<Coins>();

        public Wallet()
        {
        }

        public void AddCoins(Coins coins)
        {
            var index = Coins.FindIndex(c => c.Value == coins.Value);

            if (index != -1)
            {
                Coins[index].Add(coins);
            }
            else
            {
                Coins.Add(coins);
                Coins.Sort((c1, c2) => c2.Value.CompareTo(c1.Value));
            }
        }

        public bool SubtractCoins(Coins coins)
        {
            var index = Coins.FindIndex(c => c.Value == coins.Value);

            if (index != -1)
            {
                return Coins[index].Subtract(coins);
            }
            else
            {
                return false;
            }
        }

        public int Balance()
        {
            int balance = 0;
            foreach (var coins in Coins)
            {
                balance += coins.Value * coins.Amount;
            }
            return balance;
        }

        public void Transfer(Wallet acceptingWallet, Coins coins)
        {
            if (coins.Amount > 0 && this.SubtractCoins(coins))
            {
                acceptingWallet.AddCoins(coins);
            }
        }

        public void Combine(Wallet incomingWallet)
        {
            incomingWallet.Coins.ForEach(c => this.AddCoins(c));
        }
    }
}
