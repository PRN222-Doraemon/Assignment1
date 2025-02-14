using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Repositories.IRepositories;
using FUNewsManagement.Services.IServices;

namespace FUNewsManagement.Services
{
    public class SystemAccountService : ISystemAccountService
    {
        // =================================
        // === Fields & Props
        // =================================

        private readonly ISystemAccountRepository _repo;

        // =================================
        // === Constructors
        // =================================

        public SystemAccountService(ISystemAccountRepository repo)
        {
            _repo = repo;
        }

        // =================================
        // === Methods
        // =================================

        public async Task<SystemAccount?> CheckLogin(string email, string password)
        {
            return await _repo.CheckLoginAsync(email, password);
        }

        public async Task<bool> AddSystemAccount(SystemAccount account)
        {
            return await _repo.AddAsync(account) != null;
        }

        public async Task<bool> DeleteSystemAccount(SystemAccount account)
        {
            return await _repo.DeleteAsync(account) != null;
        }

        public async Task<SystemAccount?> GetAccountById(short accountId)
        {
            return await _repo.GetAsync(a => a.AccountId == accountId);
        }

        public async Task<List<SystemAccount>> GetAllAccounts(string? searchName = null)
        {
            return (List<SystemAccount>)await _repo
                .GetAllAsync(s => string.IsNullOrEmpty(searchName) || s.AccountName!.Contains(searchName));
        }

        public async Task<bool> UpdateSystemAccount(SystemAccount account)
        {
            return await _repo.UpdateAsync(account) != null;
        }
    }
}
