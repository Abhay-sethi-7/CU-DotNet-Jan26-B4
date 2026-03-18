using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wealth_Track.Models;

namespace Wealth_Track.Data
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext (DbContextOptions<PortfolioContext> options)
            : base(options)
        {
        }

        public DbSet<Wealth_Track.Models.Investment> Investment { get; set; } = default!;
    }
}
