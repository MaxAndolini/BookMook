namespace BookMook
{
    internal class Rentee : Customer
    {
        private List<Reservation> MyReservations = new();

        public Rentee(int id, string name, string emailAddress, string password, Wallet wallet, CreditCard creditCard) : base(id, name, emailAddress, password, wallet, creditCard)
        {
        }

        public void ShowMyReservationList()
        {
            for (int i = 0; i < MyReservations.Count; i++)
            {
                Console.WriteLine(i + "- " + MyReservations[i].GetShortInfo());
            }
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
                Console.WriteLine("--------Menu for " + Name + "----------\n" +
                "1- Show All Places\n" +
                "2- Show My Reservations\n" +
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
                        if (selection == "0")
                        {
                            break;
                        }
                        if (Int32.Parse(selection) <= ReservationManager.GetPlaceList().Count)
                        {
                            Console.WriteLine(ReservationManager.GetPlaceList()[Int32.Parse(selection) - 1].ToString());
                            Console.WriteLine("Do you want to check availability of this place ?(1 for YES, 2 for NO)");

                            string selection2 = Console.ReadLine();
                            if (selection2 == "1")
                            {
                                //Burada başlangıç ve çıkış tarihine göre kontrol edip sonrasında reservasyonu gerçekleştirilecek kod yazılmalı.

                                /*
                                Console.WriteLine("Please write down the star date and end date of your reservation\n");
                                Console.WriteLine("Start date :");
                                string Date = Console.ReadLine();
                                string format = "dd-MM-yyyy";
                                DateTime startDate = DateTime.ParseExact(Date, format, CultureInfo.InvariantCulture);

                                Console.WriteLine("End date :");
                                string date = Console.ReadLine();
                             
                                DateTime endDate = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
                                ReservationManager.GetAvailablePlaces(
                                    ReservationManager.GetPlaceList()[Int32.Parse(selection) - 1].GetAddress(), startDate, endDate);
                                ReservationManager.AddReservation(ReservationManager.GetReservation(ReservationManager.GetPlaceList()[Int32.Parse(selection) - 1].GetId()));
                               */
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
                    ShowMyReservationList();
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Select reservation to see the information! Enter selection:");
                    string selection = Console.ReadLine();
                    if (Int32.Parse(selection) <= MyReservations.Count)
                    {
                        Console.WriteLine(MyReservations[Int32.Parse(selection) - 1].ToString());
                        Console.WriteLine("Do you want to cancel this reservation ? (1 for YES, 2 for NO)");  // Maybe we can add changing the reservtion details option later(such as Start or End dates)
                        string selection2 = Console.ReadLine();
                        if (selection2 == "1")
                        {
                            ReservationManager.RemoveReservation(MyReservations[Int32.Parse(selection) - 1]);
                            MyReservations.Remove(MyReservations[Int32.Parse(selection) - 1]);
                        }
                        else
                        {
                            //Leave empty for now
                        }
                    }

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
            return "\x1b[31;1;4m-----------Rentee (" + Id + ")---------\u001b[37;24m" +
                "\nName:" + Name +
                "\nEmail address: " + EmailAddress +
                "\n\x1b[31;1;4m------------------------------\x1b[37;24m";
        }
    }
}
