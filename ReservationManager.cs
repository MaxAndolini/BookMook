namespace BookMook
{
    internal class ReservationManager
    {
        private static List<Place> PlaceList = new();
        private static List<Reservation> ReservationList = new();

        public static void PrintPlaceList()
        {
            Console.WriteLine("-----------All Places---------");
            for (int i = 0; i < PlaceList.Count; i++)
            {
                Console.WriteLine((i + 1) + "- " + PlaceList[i].GetName() + "(" + PlaceList[i].GetAddress() + ")");
            }
            Console.WriteLine("------------------------------");
        }

        public static void AddPlace(Place place)
        {
            if (PlaceList.Contains(place))
            {
                Console.WriteLine(place.GetName() + " is already in place list!");
                return;
            }

            PlaceList.Add(place);
            Console.WriteLine(place.GetName() + " is successfully added to the list ✓");
        }

        public static void RemovePlace(Place place)
        {
            if (!PlaceList.Contains(place))
            {
                Console.WriteLine(place.GetName() + " is already not in place list!");
                return;
            }

            PlaceList.Remove(place);
            Console.WriteLine(place.GetName() + " is successfully removed in the list ✓");
        }

        public static void RemovePlace(int placeId)
        {
            var place = GetPlace(placeId);

            if (place == null)
            {
                Console.WriteLine("Place couldn't found!");
                return;
            }

            RemovePlace(place);
        }

        public static Place? GetPlace(int placeId)
        {
            return PlaceList.FirstOrDefault(m => m.GetId() == placeId);
        }

        public static void AddReservation(Reservation reservation)
        {
            if (ReservationList.Contains(reservation))
            {
                Console.WriteLine("Reservation Id (" + reservation.GetId() + ") is already done!");
                return;
            }

            ReservationList.Add(reservation);
            Console.WriteLine("Reservation Id (" + reservation.GetId() + ") is successfully done ✓");
        }

        public static void RemoveReservation(Reservation reservation)
        {
            if (!ReservationList.Contains(reservation))
            {
                Console.WriteLine("Reservation Id (" + reservation.GetId() + ") isn't already exist!");
                return;
            }

            ReservationList.Remove(reservation);
            Console.WriteLine("Reservation Id (" + reservation.GetId() + ") is successfully removed ✓");
        }

        public static void RemoveReservation(int reservationId)
        {
            var reservation = GetReservation(reservationId);

            if (reservation == null)
            {
                Console.WriteLine("Reservation couldn't found!");
                return;
            }

            RemoveReservation(reservation);
        }

        public static Reservation? GetReservation(int reservationId)
        {
            return ReservationList.FirstOrDefault(m => m.GetId() == reservationId);
        }

        public static List<Place> GetAvailablePlaces(string address, DateTime startDate, DateTime endDate)
        {
            return PlaceList.Where(p => !ReservationList.Any(r => r.GetPlace() == p && r.GetStartDate() >= startDate && r.GetEndDate() < endDate)
                                                                  && p.GetAddress().Contains(address)).ToList();
        }

        public static List<Place> GetAvailablePlacesWithFilter(PlaceFilter placeFilter)
        {
            return PlaceList.Where(p => !ReservationList.Any(r => r.GetPlace() == p && (placeFilter.StartDate != null && r.GetStartDate() >= placeFilter.StartDate) && (placeFilter.EndDate != null && r.GetEndDate() < placeFilter.EndDate)) &&
            (placeFilter.Address != null && p.GetAddress().Contains(placeFilter.Address)) &&
            (placeFilter.GuestNumber != null && p.GetGuestLimit() <= placeFilter.GuestNumber) &&
            (placeFilter.RoomNumber != null && p.GetRoomNumber() == placeFilter.RoomNumber) &&
            (placeFilter.FreeWifi != null && p.GetHasFreeWifi() == placeFilter.FreeWifi) &&
            (placeFilter.SpareBathroom != null && p.GetHasSpareBathroom() == placeFilter.SpareBathroom) &&
            (placeFilter.HasPool != null && p.GetHasPool() == placeFilter.HasPool) &&
            (placeFilter.HasGarden != null && p.GetHasGarden() == placeFilter.HasGarden) &&
            (placeFilter.PrivateBeach != null && p.GetHasPrivateBeach() == placeFilter.PrivateBeach) &&
            (placeFilter.ParkingArea != null && p.GetHasParkingArea() == placeFilter.ParkingArea)).ToList();
        }

        public static List<Reservation> GetReservationList()
        {
            return ReservationList;
        }

        public static List<Place> GetPlaceList()
        {
            return PlaceList;
        }
    }
}
