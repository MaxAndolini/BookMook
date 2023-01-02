using System.Text.RegularExpressions;
using static BookMook.Utils;

namespace BookMook
{
    internal class Utils
    {
        public enum PlaceSort
        {
            TypeAscending,
            TypeDescending,
            NameAscending,
            NameDescending,
            PriceAscending,
            PriceDescending
        }

        public static PlaceType[] CustomPlaceTypeOrder = new[] {
            PlaceType.HotelRoom,
            PlaceType.Apartment
        };

        public enum ReservationSort
        {
            PlaceTypeAscending,
            PlaceTypeDescending,
            PlaceNameAscending,
            PlaceNameDescending,
            StartDateAscending,
            StartDateDescending,
            EndDateAscending,
            EndDateDescending,
            TotalPriceAscending,
            TotalPriceDescending
        }

        public static List<Place> Sort(PlaceSort sort, List<Place> places, bool message = false)
        {
            switch (sort)
            {
                case PlaceSort.TypeAscending:
                    places = places.OrderBy(a => Array.IndexOf(CustomPlaceTypeOrder, a.GetType())).ToList();
                    break;
                case PlaceSort.TypeDescending:
                    places = places.OrderByDescending(a => Array.IndexOf(CustomPlaceTypeOrder, a.GetType())).ToList();
                    break;
                case PlaceSort.NameAscending:
                    places = places.OrderBy(a => a.GetName()).ToList();
                    break;
                case PlaceSort.NameDescending:
                    places = places.OrderByDescending(a => a.GetName()).ToList();
                    break;
                case PlaceSort.PriceAscending:
                    places = places.OrderBy(a => a.GetPrice()).ToList();
                    break;
                case PlaceSort.PriceDescending:
                    places = places.OrderByDescending(a => a.GetPrice()).ToList();
                    break;
            }

            if (message) Console.WriteLine("Places sorted by " + Regex.Replace(sort.ToString(), "([a-z])_?([A-Z])", "$1 $2") + ".");

            return places;
        }

        public static List<Reservation> Sort(ReservationSort sort, List<Reservation> reservations, bool message = false)
        {
            switch (sort)
            {
                case ReservationSort.PlaceTypeAscending:
                    reservations = reservations.OrderBy(a => Array.IndexOf(CustomPlaceTypeOrder, a.GetPlace().GetType())).ToList();
                    break;
                case ReservationSort.PlaceTypeDescending:
                    reservations = reservations.OrderByDescending(a => Array.IndexOf(CustomPlaceTypeOrder, a.GetPlace().GetType())).ToList();
                    break;
                case ReservationSort.PlaceNameAscending:
                    reservations = reservations.OrderBy(a => a.GetPlace().GetName()).ToList();
                    break;
                case ReservationSort.PlaceNameDescending:
                    reservations = reservations.OrderByDescending(a => a.GetPlace().GetName()).ToList();
                    break;
                case ReservationSort.StartDateAscending:
                    reservations = reservations.OrderBy(a => a.GetStartDate()).ToList();
                    break;
                case ReservationSort.StartDateDescending:
                    reservations = reservations.OrderByDescending(a => a.GetStartDate()).ToList();
                    break;
                case ReservationSort.EndDateAscending:
                    reservations = reservations.OrderBy(a => a.GetEndDate()).ToList();
                    break;
                case ReservationSort.EndDateDescending:
                    reservations = reservations.OrderByDescending(a => a.GetEndDate()).ToList();
                    break;
                case ReservationSort.TotalPriceAscending:
                    reservations = reservations.OrderBy(a => a.GetTotalPrice()).ToList();
                    break;
                case ReservationSort.TotalPriceDescending:
                    reservations = reservations.OrderByDescending(a => a.GetTotalPrice()).ToList();
                    break;
            }

            if (message) Console.WriteLine("Reservations sorted by " + Regex.Replace(sort.ToString(), "([a-z])_?([A-Z])", "$1 $2") + ".");

            return reservations;
        }

        public static void Question(string message)
        {
            Console.WriteLine("\x1b[36;1m[?] \u001b[37;24m" + message);
        }

        public static void Info(string message)
        {
            Console.WriteLine("\x1b[32;1m[*] \u001b[37;24m" + message);
        }

        public static void Error(string message)
        {
            Console.WriteLine("\x1b[31;1m[!] \u001b[37;24m" + message);
        }

        public static dynamic? ReadLine(string question, Type desiredType)
        {
            Question(question);

            Console.CursorVisible = true;

            string? input = Console.ReadLine();

            Console.CursorVisible = false;

            try
            {
                if (desiredType == typeof(int))
                {
                    if (int.TryParse(input, out int intResult))
                    {
                        return intResult;
                    }
                }
                else if (desiredType == typeof(float))
                {
                    if (float.TryParse(input, out float floatResult))
                    {
                        return floatResult;
                    }
                }
                else if (desiredType == typeof(bool))
                {
                    if (bool.TryParse(input, out bool boolResult))
                    {
                        return boolResult;
                    }
                }
                else
                {
                    return input;
                }
            }
            catch (Exception ex)
            {
                Error("An error occurred: " + ex.Message);
                return null;
            }

            return null;
        }
    }
}
