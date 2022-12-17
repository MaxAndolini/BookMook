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
            while (true)
            {
                Console.WriteLine("--------Menu for "+Name+"----------\n" +
                "1- Show All Places\n" +
                "2- Show My Reservations\n" +
                "3- Cancel Reservation\n" +
                "0- To Quit\n" +
                "----------------------\n" +
                "Please select one of the options");
                String input = Console.ReadLine();
                if (input == "1")
                {
                    while (true)
                    {
                        ReservationManager.PrintPlaceList();
                        Console.WriteLine("----------------------");
                        Console.WriteLine("Select any place to see the detailed information!(0 to quit) Enter selection:");
                        string selection = Console.ReadLine();
                        if(selection == "0")
                        {
                            break;
                        }
                        if (Int32.Parse(selection) <= ReservationManager.GetPlaceList().Count)
                        {
                            Console.WriteLine(ReservationManager.GetPlaceList()[Int32.Parse(selection) - 1].ToString());
                            Console.WriteLine("Do you want to reserve this place ?(1 for YES, 2 for NO)");
                           
                            string selection2 = Console.ReadLine();
                            if(selection2 == "1")
                            {
                                //Burada başlangıç ve çıkış tarihine göre kontrol edip sonrasında reservasyonu gerçekleştirilecek kod yazılmalı.
                            }
                            else
                            {
                                //Boş kalsın
                            }

                        }
                        Console.WriteLine("----------------------");
                    }
                    
                }
                
                else if (input == "2")
                {
                    int counter = 0;
                    foreach (Reservation reservation in MyReservations)
                    {
                            Console.WriteLine((counter + 1) + "- " + reservation.GetShortInfo());
                        counter++;
                    }
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Select reservation to see the information! Enter selection:");
                    string selection = Console.ReadLine();
                    if (Int32.Parse(selection) <= MyReservations.Count)
                    {
                        Console.WriteLine(MyReservations[Int32.Parse(selection) - 1].ToString());
                    }

                }
                else if (input == "3")
                {

                }
                
                else if (input == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Input!!!!!!!!!");
                }


            }
        }

        public string GetInformation()
        {
            return ("------Rentee(id:" + Id + ")-----" +
                "\nName:" + Name + 
                "\nEmail address: " + EmailAddress +
                "\n----------------------\n");
        }
    }
}
