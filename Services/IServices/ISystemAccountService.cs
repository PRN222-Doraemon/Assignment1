using FUNewsManagement.BusinessObjects;

namespace FUNewsManagement.Services.IServices
{
    public interface ISystemAccountService
    {
        Task<SystemAccount?> CheckLogin(string email, string password);
        Task<SystemAccount?> GetAccountById(short accountId);
        Task<List<SystemAccount>> GetAllAccounts(string? searchName = null);
        List<int> GetAllRoles();
        Task<bool> AddSystemAccount(SystemAccount account);
        Task<bool> UpdateSystemAccount(SystemAccount account);
        Task<bool> DeleteSystemAccount(SystemAccount account);
    }
}
