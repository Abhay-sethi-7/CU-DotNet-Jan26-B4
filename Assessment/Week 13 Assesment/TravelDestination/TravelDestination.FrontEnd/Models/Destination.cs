

namespace TravelDestination.FrontEnd.Models
{
    public class Destination
    {
        
        public int DestinationID { get; set; }
        public string CityName { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public DateTime LastVisited { get; set; }
        public int Rating { get; set; }
    }
}
