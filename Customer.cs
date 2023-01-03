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
            Menu walletMenu = new("My Wallet (Balance: " + Wallet.Balance + ")", new string[] { "Deposit money from Credit Card to Wallet", "Withdraw money from Wallet to your Credit Card", "Skip" });
            int walletIndex = walletMenu.Run();

            if (walletIndex == 0)
            {
                while (true)
                {
                    try
                    {
                        var select = Utils.ReadLine("How much money do you want to deposit? \x1b[32m(Quit: -1)\x1b[37m:", typeof(int));
                        if (select == null) throw new Exception("You must write a number!");
                        if (select == -1)
                        {
                            break;
                        }
                        if (select < 0) throw new Exception("Money can't be less than 0!");

                        CreditCard.DepositToWallet(Wallet, select);

                        Console.Clear();
                        Utils.Info(select + " money successfully added to your wallet.");
                        Thread.Sleep(1000);
                        Console.Clear();

                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Utils.Error(ex.Message);
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                }
            }
            else if (walletIndex == 1)
            {
                while (true)
                {
                    try
                    {
                        var select = Utils.ReadLine("How much money do you want to withdraw? \x1b[32m(Quit: -1)\x1b[37m:", typeof(int));
                        if (select == null) throw new Exception("You must write a number!");
                        if (select == -1)
                        {
                            break;
                        }
                        if (select < 0) throw new Exception("Money can't be less than 0!");
                        if (Wallet.Balance < select) throw new Exception("You don't have that much money in your wallet!");

                        CreditCard.WithdrawFromWallet(Wallet, select);

                        Console.Clear();
                        Utils.Info(select + " money successfully withdrawn from your wallet.");
                        Thread.Sleep(1000);
                        Console.Clear();

                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.Clear();
                        Utils.Error(ex.Message);
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                }
            }
        }
    }
}
