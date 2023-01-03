namespace BookMook
{
    [Serializable]
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
            Id++;
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
        public virtual void ShowMyWallet()
        {
            Console.WriteLine("\x1b[31;1;4m-----------My Wallet(Balance : "+Wallet.Balance+")---------\u001b[37;24m");
            Console.WriteLine("1- Deposit money from Credit Card to Wallet");
            Console.WriteLine("2- Withdraw money from Wallet to your Credit Card");
            Console.WriteLine("\x1b[31;1;4m------------------------------\x1b[37;24m");
            var select = Utils.ReadLine("Select one of the options \u001b[32m(Quit: -1)\x1b[37m:", typeof(int));
            if (select == -1) return;
            else if(select == 1)
            {
                var amount = Utils.ReadLine("How much money do you want to deposit? \u001b[32m(Quit: -1)\x1b[37m:", typeof(int));
                CreditCard.DepositToWallet(Wallet, amount);
                Utils.Info("The " + amount + " of money successfully Added to your wallet!!!");
                
            }
            else if(select == 2)
            {
               
                var amount = Utils.ReadLine("How much money do you want to withdraw? \u001b[32m(Quit: -1)\x1b[37m:", typeof(int));
                if(amount <= Wallet.Balance)
                {
                    CreditCard.WithdrawFromWallet(Wallet, amount);
                    Utils.Info("The " + amount + " of money successfully withdrawed from your wallet!!!");
                }
                else
                {
                    Utils.Error("There is no enough money in the wallet");
                }
                
                
            }
            else
            {
                throw new Exception("You must enter a valid input!");
            }
            Utils.Info("Press any key to proceed...");
            Console.ReadKey(true);
        }
    }
}
