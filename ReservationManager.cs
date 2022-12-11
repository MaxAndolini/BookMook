namespace BookMook
{
    internal class ReservationManager
    {
        private List<Place> PlaceList = new List<Place>();
        private List<Reservation> ReservationList = new List<Reservation>();

        public void AddPlace(Place place)
        {
            PlaceList.Add(place);
        }

        public void RemovePlace(Place place)
        {
            PlaceList.Remove(place);
        }

        public void AddReservation(Reservation reservation)
        {
            ReservationList.Add(reservation);
        }

        public void RemoveReservation(Reservation reservation)
        {
            ReservationList.Remove(reservation);
        }

        public void CancelReservation(int reservationId)
        {
            ReservationList.RemoveAll(r => r.GetId() == reservationId);
        }

        public Reservation? GetReservation(int reservationId)
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

        public List<Place> GetAvailablePlaces(string address, DateTime startDate, DateTime endDate)
        {
            return (List<Place>)PlaceList.Where(p => !ReservationList.Any(r => r.GetPlace() == p && r.GetStartDate() >= startDate && r.GetEndDate() < endDate) && p.GetAddress().Contains(address));
        }

        public List<Place> GetAvailablePlacesWithFilter(PlaceFilter placeFilter)
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
    }
}
