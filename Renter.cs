namespace BookMook
{
    internal class Renter : Customer
    {
        private List<Place> MyPlaces;

        public Renter(int id, string name, string emailAddress, string password, Wallet wallet, CreditCard creditCard, List<Place> myPlaces) : base(id, name, emailAddress, password, wallet, creditCard)
        {
            MyPlaces = myPlaces;
        }

        public void PrintMyPlaces()
        {
            foreach (Place place in MyPlaces)
            {
                Console.WriteLine(place);
            }
        }

        public void ShowMyReservations()
        {

        }

        public override void ShowMenu()
        {

        }
    }
}
