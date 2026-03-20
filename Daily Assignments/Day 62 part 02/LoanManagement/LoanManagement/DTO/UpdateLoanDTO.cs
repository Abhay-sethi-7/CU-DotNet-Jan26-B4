using System.ComponentModel.DataAnnotations;

namespace LoanManagement.DTO
{
    public class UpdateLoanDto
    {
        [Required]
        public string BorrowerName { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int LoanTermMonths { get; set; }

        public bool IsApproved { get; set; }
    }
}