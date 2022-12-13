using System.Numerics;

namespace BookMook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Place place1 = new(0, PlaceType.HotelRoom, "Test Room", 500, "Address", 2, 1, 2, true, false, false, false, true, false, false);
            ReservationManager.AddPlace(place1);

            Wallet wallet1 = new(0, 500);
            CreditCard creditCard1 = new(0, CreditCardBrand.Visa, "4282-9096-5612-5450", "Test Deneme", new DateTime(2024, 05, 31), 026, 031);

            Renter renter = new(0, "Test", "test@gmail.com", "123", wallet1, creditCard1, new List<Place>());
            renter.ShowMenu();
            renter.ShowMenu();
            renter.ShowMenu();
        }
    }
}