using Microsoft.EntityFrameworkCore;
    using TravelDestination.Backend.Models;
namespace TravelDestination.Backend.Data
{
    
 
        public class AppDbContext : DbContext
        {
            
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<Destination> Destinations { get; set; }

            
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<Destination>(entity =>
                {
                   
                    entity.HasKey(d => d.DestinationID);

                    entity.Property(d => d.CityName)
                          .IsRequired();

                    entity.Property(d => d.Country)
                          .IsRequired();

                    entity.Property(d => d.Description)
                          .HasMaxLength(200);

                    entity.Property(d => d.Rating)
                          .HasDefaultValue(3);

                  
                });
            }
        }
    }

