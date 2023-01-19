namespace VendingMachine
{
    /// <summary>
    /// ChangeCalculator is used to find how many coins from the wallet can be paid. 
    /// In case of lack of coins remaining change is more than zero.
    /// </summary>
    public static class ChangeCalculator
    {
        public static Wallet Calculate(Wallet payingWallet, int change, out int remainingChange)
        {
            var changeWallet = new Wallet();
            foreach (var coins in payingWallet.Coins)
            {
                if (change == 0)
                {
                    break;
                }
                else
                {
                    var tranfearCoins = TransfearCoins(payingWallet, coins, change);

                    if (tranfearCoins is not null)
                    {
                        payingWallet.Transfer(changeWallet, tranfearCoins);
                        change -= tranfearCoins.Balance();
                    }
                }
            }
            remainingChange = change;
            return changeWallet;
        }

        private static Coins TransfearCoins(Wallet machineWalet, Coins coins, int change)
        {
            var transfearAmount = change / coins.Value;
            if (transfearAmount > 0)
            {
                if (transfearAmount <= coins.Amount)
                {
                    return new Coins(coins.Value, transfearAmount);
                }
                else
                {
                    return new Coins(coins);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
