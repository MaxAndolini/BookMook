namespace BookMook
{
    internal class Rentee : Customer
    {
        private List<Reservation> MyReservations;

        public Rentee(int id, string name, string emailAddress, string password, Wallet wallet, CreditCard creditCard, List<Reservation> myReservations) : base(id, name, emailAddress, password, wallet, creditCard)
        {
            MyReservations = myReservations;
        }

        public void PrintMyReservations()
        {
            foreach (Reservation reservation in MyReservations)
            {
                Console.WriteLine(reservation);
            }
        }

        public void ShowMyReservationList()
        {

        }

        public void MakeComment(Place place, string comment)
        {
            if (place != null)
            {
                place.MakeComment(comment);
            }
        }

        public void RatePlace(Place place, int rate)
        {
            if (place != null)
            {
                place.RatePlace(rate);
            }
        }

        public override void ShowMenu()
        {

        }
    }
}
