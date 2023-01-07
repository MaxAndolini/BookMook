namespace BookMook
{
    [Serializable]
    internal class ReservationManager
    {
        public static List<Renter> RenterList = new();
        public static List<Rentee> RenteeList = new();
        public static List<Place> PlaceList = new();
        public static List<Reservation> ReservationList = new();

        public static List<Place> PrintPlaceList(Utils.PlaceSort? sort = null)
        {
            List<Place> list = (sort != null) ? Utils.Sort((Utils.PlaceSort)sort, PlaceList) : PlaceList;

            Console.WriteLine("\x1b[31;1;4m-----------All Places---------\x1b[37;24m");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(i + ": " + list[i].GetName() + " (" + list[i].GetAddress() + ")");
            }
            Console.WriteLine("\x1b[31;1;4m------------------------------\x1b[37;24m");

            return list;
        }

        public static void AddPlace(Place place, bool message = false)
        {
            if (PlaceList.Contains(place))
            {
                if (message) Utils.Error(place.GetName() + " is already in place list!");
                return;
            }
            PlaceList.Add(place);
            if (message) Utils.Info(place.GetName() + " is successfully added to the list.");
        }

        public static void RemovePlace(Place place, bool message = false)
        {
            if (!PlaceList.Contains(place))
            {
                if (message) Utils.Error(place.GetName() + " is already not in place list!");
                return;
            }

            PlaceList.Remove(place);
            if (message) Utils.Info(place.GetName() + " is successfully removed in the list.");
        }

        public static void RemovePlace(int placeId, bool message = false)
        {
            var place = GetPlace(placeId);

            if (place == null)
            {
                if (message) Utils.Error("Place couldn't found!");
                return;
            }

            RemovePlace(place, message);
        }

        public static Place? GetPlace(int placeId)
        {
            return PlaceList.FirstOrDefault(m => m.GetId() == placeId);
        }

        public static void AddReservation(Reservation reservation, bool message = false)
        {
            if (ReservationList.Contains(reservation))
            {
                if (message) Utils.Error("Reservation Id (" + reservation.GetId() + ") is already done!");
                return;
            }

            ReservationList.Add(reservation);
            if (message) Utils.Info("Reservation Id (" + reservation.GetId() + ") is successfully done.");
        }

        public static void RemoveReservation(Reservation reservation, bool message = false)
        {
            if (!ReservationList.Contains(reservation))
            {
                if (message) Utils.Error("Reservation Id (" + reservation.GetId() + ") isn't already exist!");
                return;
            }

            double fee = reservation.GetTotalPrice() - reservation.GetTotalPrice() * 0.9;
            reservation.GetRentee().GetWallet().IncreaseMoney(reservation.GetTotalPrice() * 0.9);
            reservation.GetRenter().GetWallet().DecreaseMoney(reservation.GetTotalPrice() * 0.9);
            reservation.GetRentee().RemoveReservation(reservation);
            ReservationList.Remove(reservation);

            if (message) Utils.Info("Reservation Id (" + reservation.GetId() + ") is successfully removed. (The " + fee + " amount of fee deducted!)");
        }

        public static void RemoveReservation(int reservationId, bool message = false)
        {
            var reservation = GetReservation(reservationId);

            if (reservation == null)
            {
                if (message) Utils.Error("Reservation couldn't found!");
                return;
            }

            RemoveReservation(reservation, message);
        }

        public static Reservation? GetReservation(int reservationId)
        {
            return ReservationList.FirstOrDefault(m => m.GetId() == reservationId);
        }

        public static List<Place> GetAvailablePlaces(DateTime startDate, DateTime endDate)
        {
            return PlaceList.Where(p => !ReservationList.Any(r => r.GetPlace() == p && r.GetStartDate() >= startDate && r.GetEndDate() < endDate)).ToList();
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
