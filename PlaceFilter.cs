namespace BookMook
{
    internal class PlaceFilter
    {
        public string? Address;
        public int? GuestNumber;
        public int? RoomNumber;
        public bool? FreeWifi;
        public bool? SpareBathroom;
        public bool? HasPool;
        public bool? HasGarden;
        public bool? PrivateBeach;
        public bool? ParkingArea;
        public DateTime? StartDate;
        public DateTime? EndDate;

        public PlaceFilter(string? address, int? guestNumber, int? roomNumber, bool? freeWifi, bool? spareBathroom, bool? hasPool, bool? hasGarden, bool? privateBeach, bool? parkingArea, DateTime? startDate, DateTime? endDate)
        {
            Address = address;
            GuestNumber = guestNumber;
            RoomNumber = roomNumber;
            FreeWifi = freeWifi;
            SpareBathroom = spareBathroom;
            HasPool = hasPool;
            HasGarden = hasGarden;
            PrivateBeach = privateBeach;
            ParkingArea = parkingArea;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
