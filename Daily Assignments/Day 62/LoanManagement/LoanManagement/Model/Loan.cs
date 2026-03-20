using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace LoanManagement.Model
{
    public class Loan
    {
        public int Id { get; set; }
        [Required]
        public string borrowerName { get; set; }
        [Required]
        public decimal amount { get; set; }
        [Required]
        public int loanTermMonths { get; set; }
        [Required]
        public bool isApproved { get; set; }
    }
  
}
