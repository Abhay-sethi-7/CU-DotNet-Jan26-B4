using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace LoanManagement.Model
{
    public class Loan
    {
        public int Id { get; set; }
       
        public string borrowerName { get; set; }
        
        public decimal amount { get; set; }
        
        public int loanTermMonths { get; set; }
      
        public bool isApproved { get; set; }
    }
  
}
