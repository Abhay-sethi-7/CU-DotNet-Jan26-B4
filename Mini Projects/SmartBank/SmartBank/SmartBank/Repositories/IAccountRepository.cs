using SmartBank.Models;

namespace SmartBank.Repositories
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(int id);
        Task<Account> AddAsync(Account account);
        Task UpdateAsync(Account account);
        Task DeleteAsync(Account account);
    }
}