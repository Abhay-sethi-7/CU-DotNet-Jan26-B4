
using SmartBank.DTOs;

using SmartBank.Models;
using SmartBank.Repositories;
using SmartBank.Services.Exceptions;
using SmartBank.Services.Helpers;

namespace SmartBank.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AccountDto>> GetAllAsync()
        {
            var accounts = await _repo.GetAllAsync();

            return accounts.Select(a => new AccountDto
            {
                Id = a.id,
                AccountNumber = a.AccountNumber,
                Name = a.Name,
                Balance = a.Balance
            }).ToList();
        }

        public async Task<AccountDto> GetByIdAsync(int id)
        {
            var account = await _repo.GetByIdAsync(id);

            if (account == null)
                throw new NotFoundException("Account not found");

            return new AccountDto
            {
                Id = account.id,
                AccountNumber = account.AccountNumber,
                Name = account.Name,
                Balance = account.Balance
            };
        }

        public async Task<AccountDto> CreateAsync(CreateAccountDto dto)
        {
            if (dto.InitialDeposit < 1000)
                throw new BadRequestException("Minimum deposit is ₹1000");

            var account = new Account
            {
                Name = dto.Name,
                Balance = dto.InitialDeposit,
                CreatedAt = DateTime.Now
            };

            await _repo.AddAsync(account);

            account.AccountNumber = AccountNumberGenerator.Generate(account.id);
            await _repo.UpdateAsync(account);

            return new AccountDto
            {
                Id = account.id,
                AccountNumber = account.AccountNumber,
                Name = account.Name,
                Balance = account.Balance
            };
        }

        public async Task DepositAsync(TransactionDto dto)
        {
            if (dto.Amount <= 0)
                throw new BadRequestException("Amount must be greater than 0");

            var account = await _repo.GetByIdAsync(dto.AccountId);

            if (account == null)
                throw new NotFoundException("Account not found");

            account.Balance += dto.Amount;
            await _repo.UpdateAsync(account);
        }

        public async Task WithdrawAsync(TransactionDto dto)
        {
            if (dto.Amount <= 0)
                throw new BadRequestException("Amount must be greater than 0");

            var account = await _repo.GetByIdAsync(dto.AccountId);

            if (account == null)
                throw new NotFoundException("Account not found");

            if (account.Balance - dto.Amount < 1000)
                throw new BadRequestException("Minimum balance ₹1000 required");

            account.Balance -= dto.Amount;
            await _repo.UpdateAsync(account);
        }

        public async Task DeleteAsync(int id)
        {
            var account = await _repo.GetByIdAsync(id);

            if (account == null)
                throw new NotFoundException("Account not found");

            await _repo.DeleteAsync(account);
        }
    }
}