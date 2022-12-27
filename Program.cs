namespace BookMook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logo();

            Wallet wallet1 = new(0, 500);
            CreditCard creditCard1 = new(0, CreditCardBrand.Visa, "4282-9096-5612-5450", "Test Deneme", new DateTime(2024, 05, 31), 026, 031);
            Renter renter = new(0, "RenterTest", "rentertest@gmail.com", "123", wallet1, creditCard1);
            Place place1 = new(0, PlaceType.HotelRoom, renter, "Test Room", 500, "Address", 2, 1, 2, true, false, false, false, true, false, false);
            ReservationManager.AddPlace(place1);

            renter.ShowMenu();

            Rentee rentee = new(0, "RenteeTest", "renteetest@gmail.com", "123", wallet1, creditCard1);
            rentee.ShowMenu();
        }

        public static void Logo()
        {
            Console.Clear();
            Console.CursorVisible = false;

            string logo = "\x1b[36m" + @"
▀█████████▄   ▄██████▄   ▄██████▄     ▄█   ▄█▄   ▄▄▄▄███▄▄▄▄    ▄██████▄   ▄██████▄     ▄█   ▄█▄ 
  ███    ███ ███    ███ ███    ███   ███ ▄███▀ ▄██▀▀▀███▀▀▀██▄ ███    ███ ███    ███   ███ ▄███▀ 
  ███    ███ ███    ███ ███    ███   ███▐██▀   ███   ███   ███ ███    ███ ███    ███   ███▐██▀   
 ▄███▄▄▄██▀  ███    ███ ███    ███  ▄█████▀    ███   ███   ███ ███    ███ ███    ███  ▄█████▀    
▀▀███▀▀▀██▄  ███    ███ ███    ███ ▀▀█████▄    ███   ███   ███ ███    ███ ███    ███ ▀▀█████▄    
  ███    ██▄ ███    ███ ███    ███   ███▐██▄   ███   ███   ███ ███    ███ ███    ███   ███▐██▄   
  ███    ███ ███    ███ ███    ███   ███ ▀███▄ ███   ███   ███ ███    ███ ███    ███   ███ ▀███▄ 
▄█████████▀   ▀██████▀   ▀██████▀    ███   ▀█▀  ▀█   ███   █▀   ▀██████▀   ▀██████▀    ███   ▀█▀ 
                                     ▀                                                 ▀         
Welcome to the BookMook, press any key to proceed...
";

            using (StringReader reader = new(logo))
            {
                string? line = string.Empty;
                int verticalStart = (Console.WindowHeight - logo.Split("\n").Length) / 2;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, verticalStart);
                        Console.WriteLine(line);
                        ++verticalStart;
                    }
                } while (line != null);
            }

            Console.ResetColor();
            Console.ReadKey(true);
        }
    }
}