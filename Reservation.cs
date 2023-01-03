namespace BookMook
{
    [Serializable]
    internal class Reservation
    {
        private int Id;
        private Renter Renter;
        private Rentee Rentee;
        private Place Place;
        private int NumberOfGuests;
        private string UserSpecialRequest;
        private DateTime StartDate;
        private DateTime EndDate;
        private double TotalPrice;

        public Reservation(int id, Renter renter, Rentee rentee, Place place, int numberOfGuests, string userSpecialRequest, DateTime startDate, DateTime endDate, double totalPrice)
        {
            Id = id;
            Renter = renter;
            Rentee = rentee;
            Place = place;
            NumberOfGuests = numberOfGuests;
            UserSpecialRequest = userSpecialRequest;
            StartDate = startDate;
            EndDate = endDate;
            TotalPrice = totalPrice;
        }

        public int GetId()
        {
            return Id;
        }

        public void SetId(int value)
        {
            Id = value;
        }

        public Renter GetRenter()
        {
            return Renter;
        }

        public void SetRenter(Renter value)
        {
            Renter = value;
        }

        public Rentee GetRentee()
        {
            return Rentee;
        }

        public void SetRentee(Rentee value)
        {
            Rentee = value;
        }

        public Place GetPlace()
        {
            return Place;
        }

        public void SetPlace(Place value)
        {
            Place = value;
        }

        public int GetNumberOfGuests()
        {
            return NumberOfGuests;
        }

        public void SetNumberOfGuests(int value)
        {
            NumberOfGuests = value;
        }

        public string GetUserSpecialRequest()
        {
            return UserSpecialRequest;
        }

        public void SetUserSpecialRequest(string value)
        {
            UserSpecialRequest = value;
        }

        public DateTime GetStartDate()
        {
            return StartDate;
        }

        public void SetStartDate(DateTime value)
        {
            StartDate = value;
        }

        public DateTime GetEndDate()
        {
            return EndDate;
        }

        public void SetEndDate(DateTime value)
        {
            EndDate = value;
        }

        public double GetTotalPrice()
        {
            return TotalPrice;
        }

        public void SetTotalPrice(double value)
        {
            TotalPrice = value;
        }

        public override string ToString()
        {
            return "\x1b[31;1;4m-----------Reservation (" + Id + ")---------\u001b[37;24m" +
                Place.ToString() +
                Renter.GetInformation() +
                Rentee.GetInformation() +
                "\n\x1b[31;1;4m------------------------------\x1b[37;24m";
        }

        public string GetShortInfo()
        {
            return "Place: " + Place.GetName() + ", Renter: " + Renter.GetName() + ", Rentee: " + Rentee.GetName() + ", Reservation Id:" + Id;
        }
    }
}
