namespace OLA
{
    class Ride
    {
        public int RideID { get; set; }
        public double Fare { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }

    class OLADriver
    {
        public int DriverID { get; set; }
        public string DriverName { get; set; }
        public string VehicleNumber { get; set; }

        public List<Ride> Rides { get; set; }


        public OLADriver()
        {
            Rides = new List<Ride>();
        }

        public void DisplayAllRides()
        {
            if (Rides.Count == 0)
            {
                Console.WriteLine("No rides found.");
                return;
            }
            double TotalFare = 0;

            foreach (Ride r in Rides)
            {
                Console.WriteLine(
                    $"RideID: {r.RideID}, From: {r.From}, To: {r.To}, Fare: {r.Fare}"
                );
                TotalFare += r.Fare;

            }
            Console.WriteLine($"Total fare : {TotalFare}");
        }
        public override string ToString()
        {
            return $"Driver Name- {DriverName} Driver Id- {DriverID} Driver Vehicle Number = {VehicleNumber}";

        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //OLADriver d1 = new OLADriver()
            //{
            //    DriverID = 1,
            //    DriverName = "x",
            //    VehicleNumber = "HP-22-AB-1234"
            //};
            //d1.Rides.Add(new Ride()
            //{
            //    RideID = 893,
            //    Fare = 290.0,
            //    From = "Chd",
            //    To = "Mohali"
            //});
            //d1.Rides.Add(new Ride()
            //{
            //    RideID = 245,
            //    Fare = 451.0,
            //    From = "delhi",
            //    To = "gurugram"
            //});
            //Console.WriteLine(d1);
            //d1.DisplayAllRides();
            List<OLADriver> drivers = new List<OLADriver>
            {
                new OLADriver
                {
                    DriverID = 1,
                    DriverName = "Ab",
                    VehicleNumber = "HP-01-AB-1234",
                    Rides = new List<Ride>
                    {
                        new Ride { RideID = 101, From = "Chd", To = "Mohali", Fare = 250 },
                        new Ride { RideID = 102, From = "Mohali", To = "Kharar", Fare = 180 }
                    }
                },
                new OLADriver
                {
                    DriverID = 2,
                    DriverName = "Rd",
                    VehicleNumber = "HP-02-CD-5678",
                    Rides = new List<Ride>
                    {
                        new Ride { RideID = 201, From = "Delhi", To = "Gurugram", Fare = 400 },
                        new Ride { RideID = 202, From = "Gurugram", To = "Noida", Fare = 350 },
                        new Ride { RideID = 203, From = "Noida", To = "Delhi", Fare = 300 }
                    }
                },
                new OLADriver
                {
                    DriverID = 3,
                    DriverName = "Sa",
                    VehicleNumber = "HP-03-EF-9012"

                }
            };


            foreach (var driver in drivers)
            {
                Console.WriteLine(driver);
                Console.WriteLine();
                driver.DisplayAllRides();
                Console.WriteLine("----------------------------------------");
            }

        }
    }
}








