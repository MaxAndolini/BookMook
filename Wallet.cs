namespace BookMook
{
    [Serializable]
    internal class Wallet
    {
        public int Id;
        public double Balance;

        public Wallet(int id, double balance)
        {
            Id = id;
            Balance = balance;
        }

        public void IncreaseMoney(double money)
        {
            Balance += money;
        }

        public void DecreaseMoney(double money)
        {
            Balance -= money;
        }
    }
}
