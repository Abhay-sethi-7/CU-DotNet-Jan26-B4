using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LoanManagement.Model;

namespace LoanManagement.Data
{
    public class LoanManagementContext : DbContext
    {
        public LoanManagementContext (DbContextOptions<LoanManagementContext> options)
            : base(options)
        {
        }

        public DbSet<LoanManagement.Model.Loan> Loan { get; set; } = default!;
    }
}
