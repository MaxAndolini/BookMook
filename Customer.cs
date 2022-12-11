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

        public virtual void ShowMenu()
        {
            Console.WriteLine("Menu");
        }
    }
}
