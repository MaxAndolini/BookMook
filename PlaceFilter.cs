namespace BookMook
{
    [Serializable]
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
    }
}
