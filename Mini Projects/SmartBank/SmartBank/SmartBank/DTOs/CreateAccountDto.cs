using System.ComponentModel.DataAnnotations;

namespace SmartBank.DTOs
{
    public class CreateAccountDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal InitialDeposit { get; set; } 
    }
}
