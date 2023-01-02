using System.Text.RegularExpressions;

namespace BookMook
{
    internal class Rentee : Customer
    {
        private List<Reservation> MyReservations = new();


        public Rentee(int id, string name, string emailAddress, string password, Wallet wallet, CreditCard creditCard) : base(id, name, emailAddress, password, wallet, creditCard)
        {
        }

        public List<Reservation> ShowMyReservationList(Utils.ReservationSort? sort = null)
        {
            List<Reservation> list = (sort != null) ? Utils.Sort((Utils.ReservationSort)sort, MyReservations) : MyReservations;

            if (list.Count > 0)
            {
                Console.WriteLine("\x1b[31;1;4m-----------My Reservations---------\u001b[37;24m");
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine(i + ": " + list[i].GetShortInfo());
                }
                Console.WriteLine("\x1b[31;1;4m------------------------------\x1b[37;24m");
            }

            return list;
        }

        public void AddReservation(Reservation reservation)
        {
            if (MyReservations.Contains(reservation))
            {
                Utils.Error("Reservation Id (" + reservation.GetId() + ") is already done!");
                return;
            }

            MyReservations.Add(reservation);
            ReservationManager.AddReservation(reservation);

            Utils.Info("Reservation Id (" + reservation.GetId() + ") is successfully done.");
        }

        public void RemoveReservation(Reservation reservation)
        {
            if (!MyReservations.Contains(reservation))
            {
                Utils.Error("Reservation Id (" + reservation.GetId() + ") isn't already exist!");
                return;
            }
            double fee = reservation.GetTotalPrice()-reservation.GetTotalPrice() * 0.9;
            reservation.GetRentee().GetWallet().IncreaseMoney(reservation.GetTotalPrice() * 0.9);
            reservation.GetRenter().GetWallet().DecreaseMoney(reservation.GetTotalPrice() * 0.9);
            MyReservations.Remove(reservation);
            ReservationManager.RemoveReservation(reservation);
            Utils.Info("Reservation Id (" + reservation.GetId() + ") is successfully removed.(The "+fee+" amount of fee deducted!)");
        }

        public void RemoveReservation(int reservationId)
        {
            var reservation = GetReservation(reservationId);

            if (reservation == null)
            {
                Utils.Error("Reservation couldn't found!");
                return;
            }

            RemoveReservation(reservation);
        }

        public Reservation? GetReservation(int reservationId)
        {
            return MyReservations.FirstOrDefault(m => m.GetId() == reservationId);
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
                Menu menu = new("Welcome " + Name + "!", new string[] { "Show All Places", "Show My Reservations","Show My Wallet", "Quit" });
                int index = menu.Run();
                if (index == 0)
                {
                    List<Place> listPlaces = new();
                    Utils.PlaceSort? sort = null;

                    if (ReservationManager.GetPlaceList().Count != 0)
                    {
                        Utils.PlaceSort[] placeSort = (Utils.PlaceSort[])Enum.GetValues(typeof(Utils.PlaceSort));
                        string[] placeSortList = Array.ConvertAll(placeSort, x => Regex.Replace(x.ToString(), "([a-z])_?([A-Z])", "$1 $2"));

                        Menu sortMenu = new("Do you want to sort place list?", placeSortList.Concat(new string[] { "Skip" }).ToArray());
                        int sortIndex = sortMenu.Run();

                        if (sortIndex != placeSort.Length)
                        {
                            Console.Clear();
                            Utils.Info(placeSortList[sortIndex] + " is selected.");
                            Thread.Sleep(1000);
                        }

                        sort = (sortIndex == placeSort.Length) ? null : placeSort[sortIndex];
                    }

                    while (true)
                    {
                        try
                        {
                            Console.Clear();

                            listPlaces = ReservationManager.PrintPlaceList(sort);

                            if (listPlaces.Count == 0)
                            {
                                Console.Clear();
                                Utils.Error("Places are empty!");
                                Thread.Sleep(1000);
                                Console.Clear();
                                break;
                            }

                            var select = Utils.ReadLine("Enter the id of place to see the detailed information \u001b[32m(Quit: -1)\x1b[37m:", typeof(int));
                            if (select == null) throw new Exception("You must write a number!");
                            if (select == -1) break;
                            if (select < 0 || listPlaces.Count - 1 < select) throw new Exception("Id (" + select + ") Place is not found!");

                            Place current_place = listPlaces[select];
                            Console.WriteLine(current_place.ToString());


                            //Here we ask if you want to book this place
                            var select2 = Utils.ReadLine("Do you want to check availability of this place(Yes: 1, No: 2) ?\u001b[32m(Quit: -1)\x1b[37m:", typeof(int));
                            if (select2 == null) throw new Exception("You must write a number!");
                            if (select2 == -1) break;
                            else if (select2 == 2) break;
                            else if (select2 == 1)
                            {
                                Console.Clear();

                                Utils.Info("-Start Date(day/month/year)-\u001b[32m(Quit: -1)\x1b");
                                var day_start = Utils.ReadLine("Please enter the DAY of your START Date", typeof(int));
                                var month_start = Utils.ReadLine("Please enter the MONTH of your START Date(January = 1, February = 2...)", typeof(int));
                                var year_start = Utils.ReadLine("Please enter the YEAR of your START Date(2022, 2023...)", typeof(int));

                                Console.Clear();

                                Utils.Info("-End Date(day/month/year)-\u001b[32m(Quit: -1)\x1b");
                                var day_end = Utils.ReadLine("Please enter the DAY of your END Date ", typeof(int));
                                var month_end = Utils.ReadLine("Please enter the MONTH of your END Date(January = 1, February = 2...) ", typeof(int));
                                var year_end = Utils.ReadLine("Please enter the YEAR of your END Date(2022, 2023...) ", typeof(int));

                                Console.Clear();
                
                                Utils.Info("Start date = "+day_start+"/"+month_start+"/"+year_start);
                                Utils.Info("End date = " + day_end + "/" + month_end + "/" + year_end);
                                Utils.Info("Your Reservation information is being checked...");


                                DateTime startDate = new DateTime(year_start, month_start, day_start);
                                DateTime endDate = new DateTime(year_end, month_end, day_end);

                                if (ReservationManager.GetAvailablePlaces(startDate, endDate).Contains(current_place)){
                                    Utils.Info("The place is Available during this period!!!");

                                    var numberOfGuests = Utils.ReadLine("Please enter the number of the guests:", typeof(int));
                                    if (numberOfGuests <= current_place.GetGuestLimit()) {
                                        Console.Clear();
                                        Utils.Info("-----------Reservation Information-------------");
                                        Utils.Info(current_place.ToString());
                                        Utils.Info("Start date = " + day_start + "/" + month_start + "/" + year_start);
                                        Utils.Info("End date = " + day_end + "/" + month_end + "/" + year_end);
                                        Utils.Info("Number of guests = " + numberOfGuests);
                                        double Price = (endDate - startDate).TotalDays * current_place.GetPrice();
                                        Utils.Info("Price = "+Price);
                                        Utils.Info("-----------------------------------------------");
                                        var select3 = Utils.ReadLine("Do you want to book this place ?(Yes: 1, No: 2):", typeof(int));
                                        if (select3 == 2) break;
                                        if(select3 == 1)
                                        {
                                            if (Wallet.Balance >= Price)
                                            {
                                                Wallet.DecreaseMoney(Price);
                                                current_place.GetRenter().GetWallet().IncreaseMoney(Price*0.9);
                                                var specialRequest = Utils.ReadLine("Please enter if you have any special request", typeof(int));
                                                Reservation new_reservation = new Reservation(ReservationManager.GetReservationList().Count==0?0:(ReservationManager.GetReservationList()[-1].GetId()+1), current_place.GetRenter(), this, current_place, numberOfGuests, specialRequest, startDate, endDate, Price);
                                                ReservationManager.AddReservation(new_reservation);
                                                MyReservations.Add(new_reservation);
                                                Utils.Info("Reservation is Successfully Made, Have a good Holiday :)");

                                            }
                                            else
                                            {
                                                Utils.Error("You do not have enough Money for this Reservation!!!");
                                            }
                                            
                                        }
                                    }
                                    else
                                    {
                                        Utils.Error("The capacity of the room exceeds!"+"(The Capacity = "+current_place.GetGuestLimit());
                                    }
                                }
                                else
                                {
                                    Utils.Error("The place is not available for these dates!");
                                }
                            }
                            Utils.Info("Press any key to proceed...");
                            Console.ReadKey(true);

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
                else if (index == 1)
                {
                    List<Reservation> listReservations = new();
                    Utils.ReservationSort? sort = null;

                    if (MyReservations.Count != 0)
                    {
                        Utils.ReservationSort[] reservationSort = (Utils.ReservationSort[])Enum.GetValues(typeof(Utils.ReservationSort));
                        string[] reservationSortList = Array.ConvertAll(reservationSort, x => Regex.Replace(x.ToString(), "([a-z])_?([A-Z])", "$1 $2"));

                        Menu sortMenu = new("Do you want to sort reservation list?", reservationSortList.Concat(new string[] { "Skip" }).ToArray());
                        int sortIndex = sortMenu.Run();

                        if (sortIndex != reservationSort.Length)
                        {
                            Console.Clear();
                            Utils.Info(reservationSortList[sortIndex] + " is selected.");
                            Thread.Sleep(1000);
                        }

                        sort = (sortIndex == reservationSort.Length) ? null : reservationSort[sortIndex];
                    }

                    while (true)
                    {
                        try
                        {
                            Console.Clear();

                            listReservations = ShowMyReservationList(sort);

                            if (listReservations.Count == 0)
                            {
                                Console.Clear();
                                Utils.Error("My reservations are empty!");
                                Thread.Sleep(1000);
                                Console.Clear();
                                break;
                            }

                            var select = Utils.ReadLine("Enter the id of place to see the detailed information \u001b[32m(Quit: -1)\x1b[37m:", typeof(int));
                            if (select == null) throw new Exception("You must write a number!");
                            if (select == -1) break;
                            if (select < 0 || listReservations.Count - 1 < select) throw new Exception("Id (" + select + ") Place is not found!");

                            Console.WriteLine(listReservations[select].ToString());
                            Utils.Info("Press any key to proceed...");
                            Console.ReadKey(true);
                            Menu deleteMenu = new("Do you want to cancel id (" + select + ") reservation?(%10 fee will be deducted)", new string[] { "Yes", "No" });
                            int deleteIndex = deleteMenu.Run();

                            if (deleteIndex == 0) RemoveReservation(listReservations[select]);

                            Utils.Info("Press any key to proceed...");
                            Console.ReadKey(true);

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
                else if (index == 2)
                {
                    ShowMyWallet();
                }
                else if (index == 3)
                {
                    break;
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
