namespace BookMook
{
    internal class Renter : Customer
    {
        private List<Place> MyPlaces = new();

        public Renter(int id, string name, string emailAddress, string password, Wallet wallet, CreditCard creditCard) : base(id, name, emailAddress, password, wallet, creditCard)
        {
        }

        public void PrintMyPlaces()
        {
            int counter = 0;
            foreach (Place place in MyPlaces)
            {
                Console.WriteLine((counter + 1) + "- " + place.GetShortInfo());
                counter++;
            }
        }

        public void ShowMyReservations()
        {

        }

        public void AddPlace(Place place)
        {
            if (MyPlaces.Contains(place))
            {
                Console.WriteLine(place.GetName() + " is already in place list!");
                return;
            }

            MyPlaces.Add(place);
            ReservationManager.GetPlaceList().Add(place);
            Console.WriteLine(place.GetName() + " is successfully added to the list ✓");
        }

        public void RemovePlace(Place place)
        {
            if (!MyPlaces.Contains(place))
            {
                Console.WriteLine(place.GetName() + " is already not in place list!");
                return;
            }

            MyPlaces.Remove(place);
            ReservationManager.GetPlaceList().Remove(place);
            Console.WriteLine(place.GetName() + " is successfully removed in the list ✓");
        }

        public void RemovePlace(int placeId)
        {
            var place = MyPlaces.FirstOrDefault(m => m.GetId() == placeId);

            if (place == null)
            {
                Console.WriteLine("Place couldn't found!");
                return;
            }

            RemovePlace(place);
        }

        public override void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("--------Menu for " + Name + "----------\n" +
                "1- Show All Places\n" +
                "2- Show My Places\n" +
                "3- Show My Reserved Places\n" +
                "4- Add new Place\n" +
                "5- Remove Place\n" +
                "6- Cancel Reservation\n" +
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

                        }
                        Console.WriteLine("----------------------");
                    }

                }
                else if (input == "2")
                {
                    
                    Console.WriteLine("------------------------------");
                    if (MyPlaces.Count > 0)
                    {
                        while (true)
                        {
                            PrintMyPlaces();
                            Console.WriteLine("----------------------");
                            Console.WriteLine("Select any place to see the detailed information(0 to quit)! Enter selection:");
                            string selection = Console.ReadLine();
                            if(selection == "0")
                            {
                                break ;
                            }
                            if (Int32.Parse(selection) <= MyPlaces.Count)
                            {
                                Console.WriteLine(MyPlaces[Int32.Parse(selection) - 1].ToString());

                            }
                            Console.WriteLine("Do you want to delete this place ? (1 for YES, 2 for NO)");  // Maybe we can add changing the reservtion details option later(such as Start or End dates)
                            string selection2 = Console.ReadLine();
                            if (selection2 == "1")
                            {
                                RemovePlace(MyPlaces[Int32.Parse(selection) - 1]);
                             
                            }
                            else
                            {
                                //Leave empty for now
                            }
                        }
                        

                    }

                }
                else if (input == "3")
                {
                    List<Reservation> allReservationsList = ReservationManager.GetReservationList();
                    List<Reservation> myReservationsList = new List<Reservation>();
                    foreach (Reservation reservation in allReservationsList)
                    {
                        int counter = 0;
                        if (reservation.GetRenter().Id == Id)
                        {
                            Console.WriteLine((counter + 1) + "- " + reservation.GetShortInfo());
                            myReservationsList.Add(reservation);
                            counter++;
                        }
                    }
                    if (myReservationsList.Count > 0)
                    {
                        Console.WriteLine("----------------------");
                        Console.WriteLine("Select reservation to see the information! Enter selection:");
                        string selection = Console.ReadLine();
                        if (Int32.Parse(selection) <= myReservationsList.Count)
                        {
                            Console.WriteLine(myReservationsList[Int32.Parse(selection) - 1].ToString());

                        }
                    }
                    else
                    {
                        Console.WriteLine("There is not any active reservation!!!!");
                    }



                }
                else if (input == "4")
                {
                    //ENUM TYPE INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("What is the type of your place ?(1 for HotelRoom, 2 for Apartment)");

                    PlaceType place_type;
                    while (true)
                    {
                        String place_type_input = Console.ReadLine();
                        if (place_type_input == "1")
                        {
                            place_type = PlaceType.HotelRoom;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else if (place_type_input == "2")
                        {
                            place_type = PlaceType.Apartment;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Enter a valid Type number!!");
                        }
                    }

                    //NAME INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("What is the name of your place ?");
                    String name;
                    while (true)
                    {
                        String name_input = Console.ReadLine();
                        if (name_input == "")
                        {
                            Console.WriteLine("You can not leave the name empty. Please enter valid name!!");
                        }
                        else
                        {
                            name = name_input;
                            Console.WriteLine("----------------------");
                            break;
                        }
                    }

                    //Price Input
                    Console.WriteLine("----------------------");
                    Console.WriteLine("What is the price per day ?(ex: 55.99)");

                    double price;
                    while (true)
                    {
                        String price_input = Console.ReadLine();
                        if (price_input == "")
                        {
                            Console.WriteLine("You can not leave the price empty. Please enter a valid price!!");
                        }
                        else
                        {
                            price = Convert.ToDouble(price_input);
                            Console.WriteLine("----------------------");
                            break;
                        }
                    }

                    //Address INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("What is the address of your place ?");
                    String address;
                    while (true)
                    {
                        String address_input = Console.ReadLine();
                        if (address_input == "")
                        {
                            Console.WriteLine("You can not leave the address empty. Please enter valid address!!");
                        }
                        else
                        {
                            address = address_input;
                            Console.WriteLine("----------------------");
                            break;
                        }
                    }

                    //Guest Limit INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("What is the guest limit of your place ?");
                    int guest_limit;
                    while (true)
                    {
                        String guest_limit_input = Console.ReadLine();
                        if (guest_limit_input == "")
                        {
                            Console.WriteLine("You can not leave the guest limit empty. Please enter valid guest limit!!");
                        }
                        else
                        {
                            guest_limit = Int32.Parse(guest_limit_input);
                            Console.WriteLine("----------------------");
                            break;
                        }
                    }

                    //FLAT NO INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("What is the flat of your place ?");
                    int flat_no;
                    while (true)
                    {
                        String flat_no_input = Console.ReadLine();
                        if (flat_no_input == "")
                        {
                            Console.WriteLine("You can not leave the flat empty. Please enter valid flat!!");
                        }
                        else
                        {
                            flat_no = Int32.Parse(flat_no_input);
                            Console.WriteLine("----------------------");
                            break;
                        }
                    }


                    //ROOM NUMBER INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("What is the room number of your place ?");
                    int room_number;
                    while (true)
                    {
                        String room_number_input = Console.ReadLine();
                        if (room_number_input == "")
                        {
                            Console.WriteLine("You can not leave the room number. Please enter valid room number!!");
                        }
                        else
                        {
                            room_number = Int32.Parse(room_number_input);
                            Console.WriteLine("----------------------");
                            break;
                        }
                    }


                    //HAS FREE WIFI TYPE INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Does your place have Free Wifi ? ?(1 for YES, 2 for NO)");

                    bool has_free_wifi;
                    while (true)
                    {
                        String has_free_wifi_input = Console.ReadLine();
                        if (has_free_wifi_input == "1")
                        {
                            has_free_wifi = true;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else if (has_free_wifi_input == "2")
                        {
                            has_free_wifi = false;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Enter a valid input number!!");
                        }
                    }

                    //HAS SPARE BATHROOM TYPE INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Does your place have spare bathroom ? ?(1 for YES, 2 for NO)");

                    bool has_spare_bathroom;
                    while (true)
                    {
                        String has_spare_bathroom_input = Console.ReadLine();
                        if (has_spare_bathroom_input == "1")
                        {
                            has_spare_bathroom = true;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else if (has_spare_bathroom_input == "2")
                        {
                            has_spare_bathroom = false;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Enter a valid input number!!");
                        }
                    }


                    //IS SMOKING ALLOWED TYPE INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Is smoking allowed in your place ? ?(1 for YES, 2 for NO)");

                    bool is_smoking_allowed;
                    while (true)
                    {
                        String is_smoking_allowed_input = Console.ReadLine();
                        if (is_smoking_allowed_input == "1")
                        {
                            is_smoking_allowed = true;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else if (is_smoking_allowed_input == "2")
                        {
                            is_smoking_allowed = false;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Enter a valid input number!!");
                        }
                    }


                    //HAS POOL TYPE INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Does your place have POOL ? ?(1 for YES, 2 for NO)");

                    bool has_pool;
                    while (true)
                    {
                        String has_pool_input = Console.ReadLine();
                        if (has_pool_input == "1")
                        {
                            has_pool = true;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else if (has_pool_input == "2")
                        {
                            has_pool = false;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Enter a valid input number!!");
                        }
                    }


                    //HAS GARDEN TYPE INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Does your place have GARDEN ? ?(1 for YES, 2 for NO)");

                    bool has_garden;
                    while (true)
                    {
                        String has_garden_input = Console.ReadLine();
                        if (has_garden_input == "1")
                        {
                            has_garden = true;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else if (has_garden_input == "2")
                        {
                            has_garden = false;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Enter a valid input number!!");
                        }
                    }

                    //HAS PRIVATE BEACH TYPE INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Does your place have PRIVATE BEACH ? ?(1 for YES, 2 for NO)");

                    bool has_private_beach;
                    while (true)
                    {
                        String has_private_beach_input = Console.ReadLine();
                        if (has_private_beach_input == "1")
                        {
                            has_private_beach = true;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else if (has_private_beach_input == "2")
                        {
                            has_private_beach = false;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Enter a valid input number!!");
                        }
                    }

                    //HAS PARKING AREA BEACH TYPE INPUT
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Does your place have PARKING AREA ? ?(1 for YES, 2 for NO)");

                    bool has_parking_area;
                    while (true)
                    {
                        String has_parking_area_input = Console.ReadLine();
                        if (has_parking_area_input == "1")
                        {
                            has_parking_area = true;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else if (has_parking_area_input == "2")
                        {
                            has_parking_area = false;
                            Console.WriteLine("----------------------");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Please Enter a valid input number!!");
                        }
                    }

                    Place new_place = new Place(Place.TotalPlaceNumber, place_type, this, name, price, address, guest_limit, flat_no, room_number, has_free_wifi, has_spare_bathroom, is_smoking_allowed, has_pool, has_garden, has_private_beach, has_parking_area);
                    AddPlace(new_place);
                    ReservationManager.AddPlace(new_place);

                }
                else if (input == "5")
                {

                }
                else if (input == "6")
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
            return ("------Renter(id:" + Id + ")-----" +
                "\nName:" + Name +
                "\nEmail address: " + EmailAddress +
                "\n----------------------\n");
        }
    }
}
