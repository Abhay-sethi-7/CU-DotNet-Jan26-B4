  using System.ComponentModel.DataAnnotations;
 using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
namespace VoltGearSystems.Models
{

    
    public class Laptop
    {
        [Required]
        public string ModelName { get; set; }

        [Required]
        public string SerialNumber { get; set; }

        [Range(1, 1000000)]
        public decimal Price { get; set; }
    }
}
