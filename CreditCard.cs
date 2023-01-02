namespace BookMook
{
    public enum CreditCardBrand
    {
        Visa,
        Mastercard,
        AmericanExpress,
        Discover,
        Jcb
    }

    internal class CreditCard
    {
        protected int Id;
        protected CreditCardBrand Brand;
        protected string Number;
        protected string HolderName;
        protected DateTime ExpirationDate;
        protected int Cvc;
        protected int PinCode;

        public CreditCard(int id, CreditCardBrand brand, string number, string holderName, DateTime expirationDate, int cvc, int pinCode)
        {
            Id = id;
            Brand = brand;
            Number = number;
            HolderName = holderName;
            ExpirationDate = expirationDate;
            Cvc = cvc;
            PinCode = pinCode;
        }

        public bool CheckValidation()
        {
            return true;
        }

        public void DepositToWallet(Wallet wallet, double price)
        {
            if (wallet != null)
            {
                wallet.IncreaseMoney(price);
            }
        }

        public void WithdrawFromWallet(Wallet wallet, double price)
        {
            if (wallet != null)
            {
                wallet.DecreaseMoney(price);
            }
        }
    }
}
