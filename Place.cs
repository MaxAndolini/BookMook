namespace BookMook
{
    public enum PlaceType
    {
        HotelRoom,
        Apartment
    }

    internal class Place
    {
        public static int totalPlaceNumber = 0;
        private int Id;
        private PlaceType Type;
        private string Name;
        private double Price;
        private string Address;
        private int GuestLimit;
        private int FlatNo;
        private int RoomNumber;
        private bool HasFreeWifi;
        private bool HasSpareBathroom;
        private bool IsSmookingAllowed;
        private bool HasPool;
        private bool HasGarden;
        private bool HasPrivateBeach;
        private bool HasParkingArea;
        private List<(DateTime, DateTime)> BookDates = new();
        private List<string> CommentList = new();
        private List<int> RateList = new();

        public Place(int id, PlaceType type, string name, double price, string address, int guestLimit, int flatNo, int roomNumber, bool hasFreeWifi, bool hasSpareBathroom, bool isSmookingAllowed, bool hasPool, bool hasGarden, bool hasPrivateBeach, bool hasParkingArea)
        {
            Id = id;
            Type = type;
            Name = name;
            Price = price;
            Address = address;
            GuestLimit = guestLimit;
            FlatNo = flatNo;
            RoomNumber = roomNumber;
            HasFreeWifi = hasFreeWifi;
            HasSpareBathroom = hasSpareBathroom;
            IsSmookingAllowed = isSmookingAllowed;
            HasPool = hasPool;
            HasGarden = hasGarden;
            HasPrivateBeach = hasPrivateBeach;
            HasParkingArea = hasParkingArea;
            totalPlaceNumber = totalPlaceNumber + 1;
        }

        public int GetGetId()
        {
            return Id;
        }

        public void SetId(int value)
        {
            Id = value;
        }

        public PlaceType GetGetType()
        {
            return Type;
        }

        public void SetType(PlaceType value)
        {
            Type = value;
        }

        public string GetName()
        {
            return Name;
        }

        public void SetName(string value)
        {
            Name = value;
        }

        public double GetPrice()
        {
            return Price;
        }

        public void SetPrice(double value)
        {
            Price = value;
        }

        public string GetAddress()
        {
            return Address;
        }

        public void SetAddress(string value)
        {
            Address = value;
        }

        public int GetGuestLimit()
        {
            return GuestLimit;
        }

        public void SetGuestLimit(int value)
        {
            GuestLimit = value;
        }

        public int GetFlatNo()
        {
            return FlatNo;
        }

        public void SetFlatNo(int value)
        {
            FlatNo = value;
        }

        public int GetRoomNumber()
        {
            return RoomNumber;
        }

        public void SetRoomNumber(int value)
        {
            RoomNumber = value;
        }

        public bool GetHasFreeWifi()
        {
            return HasFreeWifi;
        }

        public void SetHasFreeWifi(bool value)
        {
            HasFreeWifi = value;
        }

        public bool GetHasSpareBathroom()
        {
            return HasSpareBathroom;
        }

        public void SetHasSpareBathroom(bool value)
        {
            HasSpareBathroom = value;
        }

        public bool GetIsSmookingAllowed()
        {
            return IsSmookingAllowed;
        }

        public void SetIsSmookingAllowed(bool value)
        {
            IsSmookingAllowed = value;
        }

        public bool GetHasPool()
        {
            return HasPool;
        }

        public void SetHasPool(bool value)
        {
            HasPool = value;
        }

        public bool GetHasGarden()
        {
            return HasGarden;
        }

        public void SetHasGarden(bool value)
        {
            HasGarden = value;
        }

        public bool GetHasPrivateBeach()
        {
            return HasPrivateBeach;
        }

        public void SetHasPrivateBeach(bool value)
        {
            HasPrivateBeach = value;
        }

        public bool GetHasParkingArea()
        {
            return HasParkingArea;
        }

        public void SetHasParkingArea(bool value)
        {
            HasParkingArea = value;
        }

        public List<(DateTime, DateTime)> GetBookDates()
        {
            return BookDates;
        }

        public void SetBookDates(List<(DateTime, DateTime)> value)
        {
            BookDates = value;
        }

        public List<string> GetCommentList()
        {
            return CommentList;
        }

        public void SetCommentList(List<string> value)
        {
            CommentList = value;
        }

        public List<int> GetRateList()
        {
            return RateList;
        }

        public void SetRateList(List<int> value)
        {
            RateList = value;
        }

        public void MakeComment(string comment)
        {
            CommentList.Add(comment);
        }

        public void RatePlace(int rate)
        {
            RateList.Add(rate);
        }

        public void PrintComments()
        {
            foreach (var comment in CommentList)
            {
                Console.WriteLine(comment);
            }
        }

        public void PrintRates()
        {
            foreach (var rate in RateList)
            {
                Console.WriteLine(rate);
            }
        }

        public override string ToString()
        {
            return "Name: " + Name;
        }
    }
}
