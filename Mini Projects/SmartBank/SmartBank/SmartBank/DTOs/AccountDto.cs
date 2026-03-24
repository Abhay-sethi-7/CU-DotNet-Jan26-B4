using System.ComponentModel.DataAnnotations;

namespace SmartBank.DTOs
{
    public class AccountDto
    {
        public int Id { get; set; }
        
        public string AccountNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Balance { get; set; }
    }
}
