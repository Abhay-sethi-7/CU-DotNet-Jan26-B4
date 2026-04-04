using TravelDestination.Backend.Models;

namespace TravelDestination.Backend.Repository
{
    public interface IDestinationRepository
    {
        Task<IEnumerable<Destination>> GetAllAsync();
        Task<Destination?> GetByIdAsync(int id);
        Task AddAsync(Destination destination);
        Task UpdateAsync(Destination destination);
        Task DeleteAsync(int id);
    }
}
