﻿using System.Text.RegularExpressions;
using static BookMook.Utils;

namespace BookMook
{
    [Serializable]
    internal class Renter : Customer
    {
        private List<Place> MyPlaces = new();
        public Renter(int id, string name, string emailAddress, string password, Wallet wallet, CreditCard creditCard) : base(id, name, emailAddress, password, wallet, creditCard)
        {
           
        }

        public List<Place> PrintMyPlaces(Utils.PlaceSort? sort = null)
        {
            List<Place> list = (sort != null) ? Utils.Sort((Utils.PlaceSort)sort, MyPlaces) : MyPlaces;

            Console.WriteLine("\x1b[31;1;4m-----------My Places---------\u001b[37;24m");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(i + ": " + list[i].GetShortInfo());
            }
            Console.WriteLine("\x1b[31;1;4m------------------------------\x1b[37;24m");

            return list;
        }

        public void AddPlace(Place place)
        {
            if (MyPlaces.Contains(place))
            {
                Utils.Error(place.GetName() + " is already in place list!");
                return;
            }

            MyPlaces.Add(place);
            ReservationManager.AddPlace(place);

            Utils.Info(place.GetName() + " is successfully added to the list.");
        }

        public void RemovePlace(Place place)
        {
            if (!MyPlaces.Contains(place))
            {
                Utils.Error(place.GetName() + " is already not in place list!");
                return;
            }

            MyPlaces.Remove(place);
            ReservationManager.RemovePlace(place);

            Utils.Info(place.GetName() + " is successfully removed in the list.");
        }

        public void RemovePlace(int placeId)
        {
            var place = GetPlace(placeId);

            if (place == null)
            {
                Utils.Error("Place couldn't found!");
                return;
            }

            RemovePlace(place);
        }

        public Place? GetPlace(int placeId)
        {
            return MyPlaces.FirstOrDefault(m => m.GetId() == placeId);
        }

        public override void ShowMenu()
        {
            while (true)
            {
                Menu menu = new(Name, new string[] { "Show All Places", "Show My Places", "Show My Reserved Places", "Add New Place","Show My Wallet", "Quit" });
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
                    List<Place> listPlaces = new();
                    Utils.PlaceSort? sort = null;

                    if (MyPlaces.Count != 0)
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

                            listPlaces = PrintMyPlaces(sort);

                            if (listPlaces.Count == 0)
                            {
                                Console.Clear();
                                Utils.Error("My places are empty!");
                                Thread.Sleep(1000);
                                Console.Clear();
                                break;
                            }

                            var select = Utils.ReadLine("Enter the id of place to see the detailed information \u001b[32m(Quit: -1)\x1b[37m:", typeof(int));
                            if (select == null) throw new Exception("You must write a number!");
                            if (select == -1) break;
                            if (select < 0 || listPlaces.Count - 1 < select) throw new Exception("Id (" + select + ") Place is not found!");

                            Console.WriteLine(listPlaces[select].ToString());

                            Menu deleteMenu = new("Do you want to delete id (" + select + ") place?", new string[] { "Yes", "No" });
                            int deleteIndex = deleteMenu.Run();

                            if (deleteIndex == 0) RemovePlace(listPlaces[select]);

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
                    List<Reservation> myReservationsList = ReservationManager.GetReservationList().Where(r => r.GetRenter().Id == Id).ToList();

                    if (myReservationsList.Count != 0)
                    {
                        Console.WriteLine("\x1b[31;1;4m-----------My Reserved Places---------\u001b[37;24m");
                        for (int i = 0; i < myReservationsList.Count; i++)
                        {
                            Console.WriteLine(i + ": " + myReservationsList[i].GetShortInfo());
                        }
                        Console.WriteLine("\x1b[31;1;4m------------------------------\x1b[37;24m");
                    }

                    while (true)
                    {
                        try
                        {
                            if (myReservationsList.Count == 0)
                            {
                                Console.Clear();
                                Utils.Error("My reserved places are empty!");
                                Thread.Sleep(1000);
                                Console.Clear();
                                break;
                            }

                            var select = Utils.ReadLine("Enter the id of place to see the detailed information \u001b[32m(Quit: -1)\x1b[37m:", typeof(int));
                            if (select == null) throw new Exception("You must write a number!");
                            if (select == -1) break;
                            if (select < 0 || myReservationsList.Count - 1 < select) throw new Exception("Id (" + select + ") Place is not found!");

                            Menu deleteMenu = new(myReservationsList[select].ToString() + "\nDo you want to cancel id (" + select + ") reservation?(%10 fee will be deducted)", new string[] { "Yes", "No" });

                            int deleteIndex = deleteMenu.Run();

                            if (deleteIndex == 0) ReservationManager.RemoveReservation(myReservationsList[select]);

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
                else if (index == 3)
                {
                    int step = 0;
                    PlaceType placeType = PlaceType.Apartment;
                    string name = "";
                    double price = 0.0;
                    string address = "";
                    int guestLimit = 0;
                    int flatNumber = 0;
                    int roomNumber = 0;
                    bool hasFreeWifi = false;
                    bool hasSpareBathroom = false;
                    bool isSmokingAllowed = false;
                    bool hasPool = false;
                    bool hasGarden = false;
                    bool hasPrivateBeach = false;
                    bool hasParkingArea = false;

                    while (true)
                    {
                        if (step == -1) break;

                        if (step == 0)
                        {
                            PlaceType[] placeTypes = (PlaceType[])Enum.GetValues(typeof(PlaceType));
                            string[] placeTypesList = Array.ConvertAll(placeTypes, x => Regex.Replace(x.ToString(), "([a-z])_?([A-Z])", "$1 $2"));

                            Menu typeMenu = new("What is the type of your place?", placeTypesList.Concat(new string[] { "Quit" }).ToArray());
                            int typeIndex = typeMenu.Run();
                            if (typeIndex == placeTypes.Length)
                            {
                                step = -1;
                                continue;
                            }
                            placeType = placeTypes[typeIndex];
                            Console.Clear();
                            Utils.Info(placeTypesList[typeIndex] + " is selected.");
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
                                    var select = Utils.ReadLine("What is the name of your place \u001b[32m(Back: -1, Quit: -2)\x1b[37m:", typeof(string));
                                    if (select == null) throw new Exception("You must write a string!");
                                    if (select == "-1")
                                    {
                                        step--;
                                        Console.Clear();
                                        break;
                                    }
                                    else if (select == "-2")
                                    {
                                        step = -1;
                                        break;
                                    }
                                    if (string.IsNullOrEmpty(select)) throw new Exception("Name can not be empty!");
                                    if (select.Length < 2 || select.Length > 32) throw new Exception("Name must be between 2-32 character!");

                                    name = select;
                                    Console.Clear();
                                    Utils.Info(select + " is selected.");
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
                                    var select = Utils.ReadLine("What is the price per day of your place (Ex: 55.99) \u001b[32m(Back: -1, Quit: -2)\x1b[37m:", typeof(string));
                                    if (select == null) throw new Exception("You must write a string!");
                                    if (select == "-1")
                                    {
                                        step--;
                                        Console.Clear();
                                        break;
                                    }
                                    else if (select == "-2")
                                    {
                                        step = -1;
                                        break;
                                    }
                                    if (string.IsNullOrEmpty(select)) throw new Exception("Price can not be empty!");
                                    if (!Regex.IsMatch(select, @"^(?!0*\.0+$)\d*(?:\.\d+)?$")) throw new Exception("Price must be double!");

                                    price = Convert.ToDouble(select);
                                    Console.Clear();
                                    Utils.Info(select + " is selected.");
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
                                    var select = Utils.ReadLine("What is the address of your place \u001b[32m(Back: -1, Quit: -2)\x1b[37m:", typeof(string));
                                    if (select == null) throw new Exception("You must write a string!");
                                    if (select == "-1")
                                    {
                                        step--;
                                        Console.Clear();
                                        break;
                                    }
                                    else if (select == "-2")
                                    {
                                        step = -1;
                                        break;
                                    }
                                    if (string.IsNullOrEmpty(select)) throw new Exception("Address can not be empty!");
                                    if (select.Length < 10 || select.Length > 60) throw new Exception("Address must be between 10-60 character!");

                                    address = select;
                                    Console.Clear();
                                    Utils.Info(select + " is selected.");
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
                            while (true)
                            {
                                try
                                {
                                    var select = Utils.ReadLine("What is the guest limit of your place \u001b[32m(Back: -1, Quit: -2)\x1b[37m:", typeof(int));
                                    if (select == null) throw new Exception("You must write a number!");
                                    if (select == -1)
                                    {
                                        step--;
                                        Console.Clear();
                                        break;
                                    }
                                    else if (select == -2)
                                    {
                                        step = -1;
                                        break;
                                    }
                                    if (select < 0) throw new Exception("Price can not be less than 0!");

                                    guestLimit = select;
                                    Console.Clear();
                                    Utils.Info(select + " is selected.");
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

                        if (step == 5)
                        {
                            while (true)
                            {
                                try
                                {
                                    var select = Utils.ReadLine("What is the flat number of your place \u001b[32m(Back: -1, Quit: -2)\x1b[37m:", typeof(int));
                                    if (select == null) throw new Exception("You must write a number!");
                                    if (select == -1)
                                    {
                                        step--;
                                        Console.Clear();
                                        break;
                                    }
                                    else if (select == -2)
                                    {
                                        step = -1;
                                        break;
                                    }
                                    if (select < 0) throw new Exception("Flat number can not be less than 0!");

                                    flatNumber = select;
                                    Console.Clear();
                                    Utils.Info(select + " is selected.");
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

                        if (step == 6)
                        {
                            while (true)
                            {
                                try
                                {
                                    var select = Utils.ReadLine("What is the room number of your place \u001b[32m(Back: -1, Quit: -2)\x1b[37m:", typeof(int));
                                    if (select == null) throw new Exception("You must write a number!");
                                    if (select == -1)
                                    {
                                        step--;
                                        Console.Clear();
                                        break;
                                    }
                                    else if (select == -2)
                                    {
                                        step = -1;
                                        break;
                                    }
                                    if (select < 0) throw new Exception("Room number can not be less than 0!");

                                    roomNumber = select;
                                    Console.Clear();
                                    Utils.Info(select + " is selected.");
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

                        if (step == 7)
                        {
                            Menu hasFreeWifiMenu = new("Does your place have free wifi?", new string[] { "Yes", "No", "Back", "Quit" });
                            int hasFreeWifiIndex = hasFreeWifiMenu.Run();

                            if (hasFreeWifiIndex == 2)
                            {
                                step--;
                                Console.Clear();
                                continue;
                            }
                            else if (hasFreeWifiIndex == 3)
                            {
                                step = -1;
                                continue;
                            }
                            hasFreeWifi = hasFreeWifiIndex == 0;
                            Console.Clear();
                            Utils.Info((hasFreeWifiIndex == 0 ? "Yes" : "No") + " is selected.");
                            Thread.Sleep(1000);
                            Console.Clear();
                            step++;
                        }

                        if (step == 8)
                        {
                            Menu hasSpareBathroomMenu = new("Does your place have a spare bathroom?", new string[] { "Yes", "No", "Back", "Quit" });
                            int hasSpareBathroomIndex = hasSpareBathroomMenu.Run();

                            if (hasSpareBathroomIndex == 2)
                            {
                                step--;
                                Console.Clear();
                                continue;
                            }
                            else if (hasSpareBathroomIndex == 3)
                            {
                                step = -1;
                                continue;
                            }
                            hasSpareBathroom = hasSpareBathroomIndex == 0;
                            Console.Clear();
                            Utils.Info((hasSpareBathroomIndex == 0 ? "Yes" : "No") + " is selected.");
                            Thread.Sleep(1000);
                            Console.Clear();
                            step++;
                        }

                        if (step == 9)
                        {
                            Menu isSmokingAllowedMenu = new("Is smoking allowed in your place?", new string[] { "Yes", "No", "Back", "Quit" });
                            int isSmokingAllowedIndex = isSmokingAllowedMenu.Run();

                            if (isSmokingAllowedIndex == 2)
                            {
                                step--;
                                Console.Clear();
                                continue;
                            }
                            else if (isSmokingAllowedIndex == 3)
                            {
                                step = -1;
                                continue;
                            }
                            isSmokingAllowed = isSmokingAllowedIndex == 0;
                            Console.Clear();
                            Utils.Info((isSmokingAllowedIndex == 0 ? "Yes" : "No") + " is selected.");
                            Thread.Sleep(1000);
                            Console.Clear();
                            step++;
                        }

                        if (step == 10)
                        {
                            Menu hasPoolMenu = new("Does your place have a pool?", new string[] { "Yes", "No", "Back", "Quit" });
                            int hasPoolIndex = hasPoolMenu.Run();

                            if (hasPoolIndex == 2)
                            {
                                step--;
                                Console.Clear();
                                continue;
                            }
                            else if (hasPoolIndex == 3)
                            {
                                step = -1;
                                continue;
                            }
                            hasPool = hasPoolIndex == 0;
                            Console.Clear();
                            Utils.Info((hasPoolIndex == 0 ? "Yes" : "No") + " is selected.");
                            Thread.Sleep(1000);
                            Console.Clear();
                            step++;
                        }

                        if (step == 11)
                        {
                            Menu hasGardenMenu = new("Does your place have a garden?", new string[] { "Yes", "No", "Back", "Quit" });
                            int hasGardenIndex = hasGardenMenu.Run();

                            if (hasGardenIndex == 2)
                            {
                                step--;
                                Console.Clear();
                                continue;
                            }
                            else if (hasGardenIndex == 3)
                            {
                                step = -1;
                                continue;
                            }
                            hasGarden = hasGardenIndex == 0;
                            Console.Clear();
                            Utils.Info((hasGardenIndex == 0 ? "Yes" : "No") + " is selected.");
                            Thread.Sleep(1000);
                            Console.Clear();
                            step++;
                        }

                        if (step == 12)
                        {
                            Menu hasPrivateBeachMenu = new("Does your place have a private beach?", new string[] { "Yes", "No", "Back", "Quit" });
                            int hasPrivateBeachIndex = hasPrivateBeachMenu.Run();

                            if (hasPrivateBeachIndex == 2)
                            {
                                step--;
                                Console.Clear();
                                continue;
                            }
                            else if (hasPrivateBeachIndex == 3)
                            {
                                step = -1;
                                continue;
                            }
                            hasPrivateBeach = hasPrivateBeachIndex == 0;
                            Console.Clear();
                            Utils.Info((hasPrivateBeachIndex == 0 ? "Yes" : "No") + " is selected.");
                            Thread.Sleep(1000);
                            Console.Clear();
                            step++;
                        }

                        if (step == 13)
                        {
                            Menu hasParkingAreaMenu = new("Does your place have a parking area?", new string[] { "Yes", "No", "Back", "Quit" });
                            int hasParkingAreaIndex = hasParkingAreaMenu.Run();

                            if (hasParkingAreaIndex == 2)
                            {
                                step--;
                                Console.Clear();
                                continue;
                            }
                            else if (hasParkingAreaIndex == 3)
                            {
                                step = -1;
                                continue;
                            }
                            hasParkingArea = hasParkingAreaIndex == 0;
                            Console.Clear();
                            Utils.Info((hasParkingAreaIndex == 0 ? "Yes" : "No") + " is selected.");
                            Thread.Sleep(1000);
                            Console.Clear();

                            Place new_place = new((ReservationManager.PlaceList.Last().GetId()+1), placeType, this, name, price, address, guestLimit, flatNumber, roomNumber, hasFreeWifi, hasSpareBathroom, isSmokingAllowed, hasPool, hasGarden, hasPrivateBeach, hasParkingArea);
                            AddPlace(new_place);

                            Utils.Info("Press any key to proceed...");
                            Console.ReadKey(true);

                            break;
                        }
                    }
                }
                else if (index == 4)
                {
                    ShowMyWallet();
                }
                else if (index == 5)
                {
                    break;
                }
            }
        }

        public string GetInformation()
        {
            return "\x1b[31;1;4m-----------Renter (" + Id + ")---------\u001b[37;24m" +
                "\nName:" + Name +
                "\nEmail address: " + EmailAddress +
                "\n\x1b[31;1;4m------------------------------\x1b[37;24m";
        }
    }
}
