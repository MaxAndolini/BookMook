namespace BookMook
{
    [Serializable]
    internal class Program
    {

        static void Main(string[] args)
        {
            Utils.Logo();
            ReservationManager.RenterList = FileManager.Read<List<Renter>>("RenterList.bin");
            ReservationManager.RenteeList = FileManager.Read<List<Rentee>>("RenteeList.bin");
            ReservationManager.PlaceList = FileManager.Read<List<Place>>("PlaceList.bin");
            ReservationManager.ReservationList = FileManager.Read<List<Reservation>>("ReservationList.bin");

            if (ReservationManager.RenterList.Count == 0)
            {
                Wallet wallet1 = new(0, 0);
                Wallet wallet2 = new(0, 50000);

                CreditCard creditCard1 = new(0, CreditCardBrand.Visa, "4321-4321-4321-4321", "Test Deneme", new DateTime(2024, 05, 31), 026, 031);
                CreditCard creditCard2 = new(0, CreditCardBrand.Visa, "1234-1234-1234-1234", "Test Deneme", new DateTime(2024, 05, 31), 026, 031);

                Renter renter = new(0, "RenterTest", "rentertest@gmail.com", "123", wallet1, creditCard1);
                ReservationManager.RenterList.Add(renter);

                Rentee rentee = new(0, "RenteeTest", "renteetest@gmail.com", "123", wallet2, creditCard2);
                ReservationManager.RenteeList.Add(rentee);

                Place place1 = new(0, PlaceType.HotelRoom, renter, "Test Room", 500, "Address", 2, 1, 2, true, false, false, false, true, false, false);
                ReservationManager.PlaceList.Add(place1);
                renter.AddPlace(place1);

            }
            else
            {
                Rentee rentee = ReservationManager.RenteeList[0];
                rentee.ShowMenu();
            }



            //rentee.ShowMenu();
            //renter.ShowMenu();

            FileManager.Write("RenterList.bin", ReservationManager.RenterList);
            FileManager.Write("RenteeList.bin", ReservationManager.RenteeList);
            FileManager.Write("PlaceList.bin", ReservationManager.PlaceList);
            FileManager.Write("ReservationList.bin", ReservationManager.ReservationList);
        }
    }
}