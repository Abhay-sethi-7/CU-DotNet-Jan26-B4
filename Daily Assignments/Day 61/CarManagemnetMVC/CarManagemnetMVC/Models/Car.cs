using System.ComponentModel.DataAnnotations;

namespace CarManagemnetMVC.Models
{
  
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }
    }
}
