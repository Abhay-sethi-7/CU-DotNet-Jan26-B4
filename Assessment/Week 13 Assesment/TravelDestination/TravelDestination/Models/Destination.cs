using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelDestination.Backend.Models
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
