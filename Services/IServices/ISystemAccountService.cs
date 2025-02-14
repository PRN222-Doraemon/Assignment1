using FUNewsManagement.BusinessObjects;

namespace FUNewsManagement.Services.IServices
{
    public interface ISystemAccountService
    {
        Task<SystemAccount?> CheckLogin(string email, string password);
        Task<SystemAccount?> GetAccountById(short accountId);
        Task<List<SystemAccount>> GetAllAccounts(string? searchName = null);
        Task<bool> AddSystemAccount(SystemAccount account);
        Task<bool> UpdateSystemAccount(SystemAccount account);
        Task<bool> DeleteSystemAccount(SystemAccount account);
    }
}
