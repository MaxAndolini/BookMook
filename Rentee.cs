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

            MyReservations.Remove(reservation);
            ReservationManager.RemoveReservation(reservation);

            Utils.Info("Reservation Id (" + reservation.GetId() + ") is successfully removed.");
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
                Menu menu = new("Welcome " + Name + "!", new string[] { "Show All Places", "Show My Reservations", "Quit" });
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

                            Console.WriteLine(listPlaces[select].ToString());

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

                            Menu deleteMenu = new("Do you want to cancel id (" + select + ") reservation?", new string[] { "Yes", "No" });
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
