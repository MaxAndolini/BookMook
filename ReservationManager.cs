namespace BookMook
{
    internal class ReservationManager
    {
        private static List<Place> PlaceList = new List<Place>();
        private static List<Reservation> ReservationList = new List<Reservation>();

        public static void PrintPlaceList()
        {
            Console.WriteLine("-----------All Places---------");
            for(int i = 0; i < PlaceList.Count; i++)
            {
                Console.WriteLine((i+1)+"- "+PlaceList[i].GetName()+"("+ PlaceList[i].GetAddress()+ ")");
            }
            Console.WriteLine("------------------------------");
        }

        public static void AddPlace(Place place)
        {
            PlaceList.Add(place);
            Console.WriteLine("The Place ADDED successfully ✓");
        }

        public static void RemovePlace(Place place)
        {
            PlaceList.Remove(place);
            Console.WriteLine("The Place REMOVED successfully ✓");
        }

        public static void AddReservation(Reservation reservation)
        {
            ReservationList.Add(reservation);
            Console.WriteLine("The Reservation is DONE ✓");
        }

        public static void RemoveReservation(Reservation reservation)
        {
            ReservationList.Remove(reservation);
            Console.WriteLine("The Reservation REMOVED successfully ✓");
        }

        public static void CancelReservation(int reservationId)
        {
            ReservationList.RemoveAll(r => r.GetId() == reservationId);
            Console.WriteLine("The Reservation Canceled successfully ✓");
        }

        public static Reservation? GetReservation(int reservationId)
        {
            foreach (Reservation reservation in ReservationList)
            {
                if (reservation.GetId() == reservationId)
                {
                    return reservation;
                }
            }

            return null;
        }

        public static List<Place> GetAvailablePlaces(string address, DateTime startDate, DateTime endDate)
        {
            return (List<Place>)PlaceList.Where(p => !ReservationList.Any(r => r.GetPlace() == p && r.GetStartDate() >= startDate && r.GetEndDate() < endDate) && p.GetAddress().Contains(address));
        }

        public static List<Place> GetAvailablePlacesWithFilter(PlaceFilter placeFilter)
        {
            return (List<Place>)PlaceList.Where(p => !ReservationList.Any(r => r.GetPlace() == p && (placeFilter.StartDate != null && r.GetStartDate() >= placeFilter.StartDate) && (placeFilter.EndDate != null && r.GetEndDate() < placeFilter.EndDate)) &&
            (placeFilter.Address != null && p.GetAddress().Contains(placeFilter.Address)) &&
            (placeFilter.GuestNumber != null && p.GetGuestLimit() <= placeFilter.GuestNumber) &&
            (placeFilter.RoomNumber != null && p.GetRoomNumber() == placeFilter.RoomNumber) &&
            (placeFilter.FreeWifi != null && p.GetHasFreeWifi() == placeFilter.FreeWifi) &&
            (placeFilter.SpareBathroom != null && p.GetHasSpareBathroom() == placeFilter.SpareBathroom) &&
            (placeFilter.HasPool != null && p.GetHasPool() == placeFilter.HasPool) &&
            (placeFilter.HasGarden != null && p.GetHasGarden() == placeFilter.HasGarden) &&
            (placeFilter.PrivateBeach != null && p.GetHasPrivateBeach() == placeFilter.PrivateBeach) &&
            (placeFilter.ParkingArea != null && p.GetHasParkingArea() == placeFilter.ParkingArea));
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
