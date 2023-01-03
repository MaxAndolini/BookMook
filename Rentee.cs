using System.Text.RegularExpressions;

namespace BookMook
{
    [Serializable]
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
                Console.WriteLine("\x1b[31;1;4m-----------My Reservations---------\x1b[37;24m");
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
            double fee = reservation.GetTotalPrice() - reservation.GetTotalPrice() * 0.9;
            reservation.GetRentee().GetWallet().IncreaseMoney(reservation.GetTotalPrice() * 0.9);
            reservation.GetRenter().GetWallet().DecreaseMoney(reservation.GetTotalPrice() * 0.9);
            MyReservations.Remove(reservation);
            ReservationManager.RemoveReservation(reservation);
            Utils.Info("Reservation Id (" + reservation.GetId() + ") is successfully removed. (The " + fee + " amount of fee deducted!)");
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
                Menu menu = new("Welcome " + Name + "!", new string[] { "Show All Places", "Show My Reservations", "Show My Wallet", "Quit" });
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

                            var select = Utils.ReadLine("Enter the id of place to see the detailed information \x1b[32m(Quit: -1)\x1b[37m:", typeof(int));
                            if (select == null) throw new Exception("You must write a number!");
                            if (select == -1) break;
                            if (select < 0 || listPlaces.Count - 1 < select) throw new Exception("Id (" + select + ") Place is not found!");

                            Place current_place = listPlaces[select];

                            int step = 0;
                            DateTime startDate = new();
                            DateTime endDate = new();
                            int numberOfGuests = 0;
                            double price = 0;

                            while (true)
                            {
                                if (step == -1) break;

                                if (step == 0)
                                {
                                    Menu checkMenu = new(current_place.ToString() + "\n\x1b[37;1;4mDo you want to check availability of this place?", new string[] { "Yes", "No" });
                                    int checkIndex = checkMenu.Run();
                                    if (checkIndex == 1)
                                    {
                                        step = -1;
                                        continue;
                                    }
                                    Console.Clear();
                                    Utils.Info((checkIndex == 0 ? "Yes" : "No") + " is selected.");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                    step++;
                                }

                                if (step == 1)
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            var select2 = Utils.ReadLine("What is the start date of your reservation (day/month/year) \x1b[32m(Back: -1, Quit: -2)\x1b[37m:", typeof(string));
                                            if (select2 == null) throw new Exception("You must write a date!");
                                            if (select2 == "-1")
                                            {
                                                step--;
                                                Console.Clear();
                                                break;
                                            }
                                            else if (select2 == "-2")
                                            {
                                                step = -1;
                                                break;
                                            }
                                            if (string.IsNullOrEmpty(select2)) throw new Exception("Date can't be empty!");
                                            DateTime? date = Utils.IsValidDate(select2);
                                            if (date == null) throw new Exception("You must write a date!");
                                            if (date < DateTime.Today) throw new Exception("You can't enter a date before today!");

                                            startDate = (DateTime)date;
                                            Console.Clear();
                                            Utils.Info(select2 + " is selected.");
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            step++;

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

                                if (step == 2)
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            var select2 = Utils.ReadLine("What is the end date of your reservation (day/month/year) \x1b[32m(Back: -1, Quit: -2)\x1b[37m:", typeof(string));
                                            if (select2 == null) throw new Exception("You must write a date!");
                                            if (select2 == "-1")
                                            {
                                                step--;
                                                Console.Clear();
                                                break;
                                            }
                                            else if (select2 == "-2")
                                            {
                                                step = -1;
                                                break;
                                            }
                                            if (string.IsNullOrEmpty(select2)) throw new Exception("Date can't be empty!");
                                            DateTime? date = Utils.IsValidDate(select2);
                                            if (date == null) throw new Exception("You must write a date!");
                                            if (date < DateTime.Today) throw new Exception("You can't enter a date before today!");
                                            if (startDate.CompareTo(endDate) < 0) throw new Exception("Start date can't be after end date!");
                                            if (!ReservationManager.GetAvailablePlaces(startDate, (DateTime)date).Contains(current_place)) throw new Exception("The place is not available for these dates!");

                                            endDate = (DateTime)date;
                                            Console.Clear();
                                            Utils.Info(select2 + " is selected.");
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            step++;

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

                                if (step == 3)
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            string text = "\x1b[31;1;4m-----------Reservation Details---------\x1b[37;24m" +
                                                current_place.ToString() + "\n" +
                                                "Start Date : " + startDate.ToString("dd/MM/yyyy") + "\n" +
                                                "End Date : " + endDate.ToString("dd/MM/yyyy") +
                                                "\n\x1b[31;1;4m------------------------------\x1b[37;24m";
                                            var select2 = Utils.ReadLine(text + "\n\x1b[37;1;4mWhat is the the number of the guests? \x1b[32m(Back: -1, Quit: -2)\x1b[37m:", typeof(int));
                                            if (select2 == null) throw new Exception("You must write a number!");
                                            if (select2 == -1)
                                            {
                                                step--;
                                                Console.Clear();
                                                break;
                                            }
                                            else if (select2 == -2)
                                            {
                                                step = -1;
                                                break;
                                            }
                                            if (select2 < 0) throw new Exception("Number of the guests can't be less than 0!");
                                            if (numberOfGuests > current_place.GetGuestLimit()) throw new Exception("The capacity of the room exceeds! (The Capacity: " + current_place.GetGuestLimit() + ")");

                                            numberOfGuests = select2;
                                            price = (endDate - startDate).TotalDays * current_place.GetPrice();
                                            Console.Clear();
                                            Utils.Info(select2 + " is selected.");
                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            step++;

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

                                if (step == 4)
                                {
                                    string text = "\x1b[31;1;4m-----------Reservation Details---------\x1b[37;24m" +
                                        current_place.ToString() + "\n" +
                                        "Start Date : " + startDate.ToString("dd/MM/yyyy") + "\n" +
                                        "End Date : " + endDate.ToString("dd/MM/yyyy") + "\n" +
                                        "Number of Guests : " + numberOfGuests + "\n" +
                                        "Price : " + price +
                                        "\n\x1b[31;1;4m------------------------------\x1b[37;24m";
                                    Menu bookMenu = new(text + "\n\x1b[37;1;4mDo you want to book this place?", new string[] { "Yes", "No", "Back", "Quit" });
                                    int bookIndex = bookMenu.Run();

                                    if (bookIndex == 2)
                                    {
                                        step--;
                                        Console.Clear();
                                        continue;
                                    }
                                    else if (bookIndex == 3)
                                    {
                                        step = -1;
                                        continue;
                                    }

                                    Console.Clear();
                                    Utils.Info((bookIndex == 0 ? "Yes" : "No") + " is selected.");
                                    Thread.Sleep(1000);
                                    Console.Clear();

                                    if (bookIndex == 0)
                                    {
                                        if (Wallet.Balance >= price)
                                        {
                                            step++;
                                        }
                                        else
                                        {
                                            Utils.Error("You do not have enough money for this reservation!");
                                            step = -1;

                                            Thread.Sleep(1000);
                                            Console.Clear();
                                        }
                                    }
                                }

                                if (step == 5)
                                {
                                    while (true)
                                    {
                                        try
                                        {
                                            var select2 = Utils.ReadLine("Please enter if you have any special request \x1b[32m(Back: -1, Quit: -2)\x1b[37m:", typeof(string));
                                            if (select2 == null) throw new Exception("You must write a string!");
                                            if (select2 == "-1")
                                            {
                                                step--;
                                                Console.Clear();
                                                break;
                                            }
                                            else if (select2 == "-2")
                                            {
                                                step = -1;
                                                break;
                                            }
                                            if (string.IsNullOrEmpty(select2)) throw new Exception("Special Request can't be empty!");

                                            Console.Clear();
                                            Utils.Info(select2 + " is selected.");
                                            Thread.Sleep(1000);
                                            Console.Clear();

                                            Wallet.DecreaseMoney(price);
                                            current_place.GetRenter().GetWallet().IncreaseMoney(price * 0.9);
                                            Reservation new_reservation = new(ReservationManager.GetReservationList().Count == 0 ? 0 : (ReservationManager.GetReservationList().Last().GetId() + 1), current_place.GetRenter(), this, current_place, numberOfGuests, select2, startDate, endDate, price);
                                            AddReservation(new_reservation);

                                            Thread.Sleep(1000);
                                            Console.Clear();
                                            step = -1;

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

                            var select = Utils.ReadLine("Enter the id of place to see the detailed information \x1b[32m(Quit: -1)\x1b[37m:", typeof(int));
                            if (select == null) throw new Exception("You must write a number!");
                            if (select == -1) break;
                            if (select < 0 || listReservations.Count - 1 < select) throw new Exception("Id (" + select + ") Place is not found!");

                            Menu deleteMenu = new(listReservations[select].ToString() + "\n\x1b[37;1;4mDo you want to cancel id (" + select + ") reservation? (%10 fee will be deducted)", new string[] { "Yes", "No" });
                            int deleteIndex = deleteMenu.Run();

                            if (deleteIndex == 0) RemoveReservation(listReservations[select]);
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
            return "\x1b[31;1;4m-----------Rentee (" + Id + ")---------\x1b[37;24m" +
                "\nName:" + Name +
                "\nEmail address: " + EmailAddress +
                "\n\x1b[31;1;4m------------------------------\x1b[37;24m";
        }
    }
}
