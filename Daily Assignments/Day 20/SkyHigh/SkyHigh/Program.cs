
using SkyHigh;

namespace SkyHigh
    {
        public class Flight :IComparable<Flight>
        {
            public string FlightNumber { get; set; }
            public decimal Price { get; set; }
            public TimeSpan Duration { get; set; }
            public DateTime DepartureTime { get; set; }

        public int CompareTo(Flight? other)
        {
            return this.Price.CompareTo(other?.Price);

        }
        public override string ToString()
        {
            return $"FlightNumber - {FlightNumber} Price - {Price} Duration- {Duration} DepartureTime- {DepartureTime}";
        }
    }
}

class DurationComparer : IComparer<Flight>
{
    public int Compare(Flight? x, Flight? y)
    {
        
        if (ReferenceEquals(x, null) && ReferenceEquals(y, null))
            return 0;
        if (ReferenceEquals(x, null))
            return -1;
        if (ReferenceEquals(y, null))
            return 1;

        return x.Duration.CompareTo(y.Duration);
    }
}
class DepartureComparer : IComparer<Flight>
{
    public int Compare(Flight? x, Flight? y)
    {
        if (ReferenceEquals(x, null) && ReferenceEquals(y, null))
            return 0;
        if (ReferenceEquals(x, null))
            return -1;
        if (ReferenceEquals(y, null))
            return 1;
        return x.DepartureTime.CompareTo(y.DepartureTime);
    }
}
internal class Program 
    {
        static void Main(string[] args)
        {
        List<Flight> flights = new List<Flight>
            {
                new Flight
                {
                    FlightNumber = "A101",
                   
                    Duration = new TimeSpan(2,30,8),
                    DepartureTime = new DateTime(2026, 2, 1, 6, 30, 0)
                },
                new Flight
                {
                    FlightNumber = "S202",
                    Price = 3200,
                    Duration = new TimeSpan(4,30,8),
                    DepartureTime = new DateTime(2026, 2, 1, 5, 45, 0)
                },
                new Flight
                {
                    FlightNumber = "U303",
                    Price = 5200,
                    Duration = new TimeSpan(2,45,8),
                    DepartureTime = new DateTime(2026, 2, 1, 18, 0, 0)
                }
            };

        Console.WriteLine("----------------Before Sorting------------------");
        foreach (var flight in flights)
        {
            Console.WriteLine(flight);

        }
        Console.WriteLine();
        flights.Sort(new DurationComparer());

        Console.WriteLine("---- Sorted by Duration [Business Runner View] ----");
        Console.WriteLine();
        foreach (var flight in flights)
        {
            Console.WriteLine(flight);
        }
        Console.WriteLine();
        flights.Sort(new DepartureComparer());

        Console.WriteLine("---- Sorted by Departure Time [Early Bird View] ----");
        Console.WriteLine();
        foreach (var flight in flights)
        {
            Console.WriteLine(flight);
        }
        Console.WriteLine();
        flights.Sort();

        Console.WriteLine("---- Sorted by Price [Economy View]----");
        Console.WriteLine();
        foreach (var flight in flights)
        {
            Console.WriteLine(flight);
        }

    }
}












