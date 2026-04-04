using Microsoft.EntityFrameworkCore;
using TravelDestination.Backend.Data;
using TravelDestination.Backend.Models;

namespace TravelDestination.Backend.Repository
{
    public class DestinationRepository : IDestinationRepository
    {
        private readonly AppDbContext _context;

        public DestinationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Destination>> GetAllAsync()
            => await _context.Destinations.ToListAsync();

        public async Task<Destination?> GetByIdAsync(int id)
            => await _context.Destinations.FindAsync(id);

        public async Task AddAsync(Destination destination)
        {
            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Destination destination)
        {
            _context.Destinations.Update(destination);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dest = await _context.Destinations.FindAsync(id);
            if (dest != null)
            {
                _context.Destinations.Remove(dest);
                await _context.SaveChangesAsync();
            }
        }
    }
}
