using System;

namespace VendingMachine
{
    /// <summary>
    /// Coins class represents number of same value coins.
    /// </summary>

    public class Coins
    {
        public int Value { get; }
        public int Amount { get; private set; }

        public Coins(int value, int amount)
        {
            Value = value > 0 ? value : throw new Exception("Wrong value");
            Amount = amount >= 0 ? amount : throw new Exception("Negative amount");
        }

        public Coins(Coins coins)
        {
            Value = coins.Value;
            Amount = coins.Amount;
        }

        public bool Add(Coins coins)
        {
            if (Value == coins.Value)
            {
                Amount += coins.Amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Subtract(Coins coins)
        {
            if (Value == coins.Value && Amount >= coins.Amount)
            {
                Amount -= coins.Amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Balance()
        {
            return Value * Amount;
        }
    }
}
