namespace BookMook
{
    internal class Customer
    {
        protected int Id;
        protected string Name;
        protected string EmailAddress;
        protected string Password;
        protected Wallet Wallet;
        protected CreditCard CreditCard;

        public Customer(int id, string name, string emailAddress, string password, Wallet wallet, CreditCard creditCard)
        {
            Id = id;
            Name = name;
            EmailAddress = emailAddress;
            Password = password;
            Wallet = wallet;
            CreditCard = creditCard;
        }

        public int GetId()
        {
            return Id;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetEmailAddress()
        {
            return EmailAddress;
        }

        public string GetPassword()
        {
            return Password;
        }

        public Wallet GetWallet()
        {
            return Wallet;
        }

        public CreditCard GetCreditCard()
        {
            return CreditCard;
        }

        public virtual void ShowMenu()
        {
            Console.WriteLine("Menu");
        }
    }
}
