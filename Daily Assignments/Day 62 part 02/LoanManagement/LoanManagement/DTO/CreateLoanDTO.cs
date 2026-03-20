using System.ComponentModel.DataAnnotations;

namespace LoanManagement.DTO
{
    public class CreateLoanDto
    {
        [Required]
        public string BorrowerName { get; set; } = string.Empty;

        [Range(1, double.MaxValue)]
        public decimal Amount { get; set; }

        [Range(1, 600)]
        public int LoanTermMonths { get; set; }
    }
}