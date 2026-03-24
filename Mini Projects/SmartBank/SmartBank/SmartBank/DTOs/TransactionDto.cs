using System.ComponentModel.DataAnnotations;

namespace SmartBank.DTOs
{
    public class TransactionDto
    {
        [Required]
        public int AccountId { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
