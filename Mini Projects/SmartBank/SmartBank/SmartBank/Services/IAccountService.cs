
using SmartBank.DTOs;

namespace SmartBank.Services
{
    public interface IAccountService
    {
        Task<List<AccountDto>> GetAllAsync();
        Task<AccountDto> GetByIdAsync(int id);
        Task<AccountDto> CreateAsync(CreateAccountDto dto);
        Task DepositAsync(TransactionDto dto);
        Task WithdrawAsync(TransactionDto dto);
        Task DeleteAsync(int id);
    }
}