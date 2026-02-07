namespace GlobalFreightTrackingSystem
{
    public interface ILoggable
    {
        public void SaveLog(string message);
    }
    public class LogManager : ILoggable
    {
        private const string FileName = @"..\..\..\shipment.log";
        public void SaveLog(string message)
        {
            using (StreamWriter sw = new StreamWriter(FileName, true))
            {
                sw.WriteLine($"{DateTime.Now} - {message}");
            }
        }
    }
    public class RestrictedDestinantionException : Exception
    {
        public string DeniedLoaction { get; set; }
        public RestrictedDestinantionException(string location)
        {
            DeniedLoaction = location;
        }
    }
    public class InsecurePackagingException : Exception
    {
        public InsecurePackagingException():base($"Packaging is insecure!!")

        {
            
        }
    }
    public abstract class Shipment
    {
        public string TrackingId { get; set; }
        public double weight { get; set; }
        public string Destination { get; set; }
        public bool IsFragile { get; set; }
        public bool HasBeenReinforced { get; set; }
        protected string[] restrictedZones = { "North Pole", "South Pole", "Unknown Island", "Antarctic Region" };
        public abstract void ProcessShipment();
        protected void Validate()
        {
            
            for (int i = 0; i < restrictedZones.Length; i++)
            {
                if (Destination == restrictedZones[i])
                {
                    throw new RestrictedDestinantionException(Destination);
                }
            }
            if (IsFragile && !HasBeenReinforced)
            {
                throw new InsecurePackagingException();
            }
            if (weight <= 0) throw new ArgumentOutOfRangeException("weight Must be greater than zero!!");
        }

    }
    public class ExpressShipment : Shipment
    {
        public override void ProcessShipment()
        {
            Validate();
            Console.WriteLine($"Express Shipment {TrackingId} processed Quickly.");
        }
    }

    public class HeavyFreight : Shipment
    {
        public bool HasHeavyLiftPermit { get; set; }
        public override void ProcessShipment()
        {
            Validate();
            if (weight > 1000 && !HasHeavyLiftPermit)
                throw new Exception("Heavy lift permit required for freight over 1000kg.");

            Console.WriteLine($"Heavy freight {TrackingId} processed successfully.");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            LogManager logger = new LogManager();
            List<Shipment> shipments = new List<Shipment>()
            {
                new ExpressShipment
                {
                    TrackingId = "Ex01",
                    weight = 10,
                    Destination= "New Delhi",
                    IsFragile = false,
                    HasBeenReinforced = false
                },
                new ExpressShipment
                {
                     TrackingId = "Ex02",
                    weight = -102,
                    Destination= "Chandigarh",
                    IsFragile = false,
                    HasBeenReinforced = false
                },
                new HeavyFreight
                {
                    TrackingId = "Hv01",
                    weight = 1002,
                    Destination= "North Pole",
                    IsFragile = true,
                    HasBeenReinforced = true,
                    HasHeavyLiftPermit = true
                },
                new HeavyFreight
                {
                     TrackingId = "Hx02",
                    weight = -1072,
                    Destination= "Mumbai",
                    IsFragile = false,
                    HasBeenReinforced = false,
                    HasHeavyLiftPermit = false
                },new ExpressShipment
                {
                    TrackingId = "Ex03",
                    weight = 20,
                    Destination = "Bangalore",
                    IsFragile = true,
                    HasBeenReinforced = false
                }

            };
            for (int i = 0; i < shipments.Count; i++)
            {
                Shipment s = shipments[i];
                try
                {
                    s.ProcessShipment();
                    logger.SaveLog($"SUCCESS: Shipment {s.TrackingId} processed.");
                }
                catch (RestrictedDestinantionException ex)
                {
                    logger.SaveLog($"SECURITY ALERT: {s.TrackingId} denied for location {ex.DeniedLoaction}");
                }
                catch (InsecurePackagingException ex)
                {
                    logger.SaveLog($"SECURITY ALERT: {s.TrackingId} - {ex.Message}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    logger.SaveLog($"DATA ENTRY ERROR: {s.TrackingId} - {ex.Message}");
                }
                catch (Exception ex)
                {
                    logger.SaveLog($"ERROR: {s.TrackingId} - {ex.Message}");
                }
                finally
                {
                    Console.WriteLine($"Processing attempt finished for ID: {s.TrackingId}");
                }
            }
        }
    }
}
