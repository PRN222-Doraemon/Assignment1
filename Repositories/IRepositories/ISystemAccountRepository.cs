using BusinessObjects;

namespace Repositories.IRepositories
{
    public interface ISystemAccountRepository
    {
        SystemAccount CheckLogin(string username, string password); 
        SystemAccount GetAccountById(short accountId); 
        IEnumerable<SystemAccount> GetAllAccounts();
        bool AddSystemAccount(SystemAccount account);
        bool UpdateSystemAccount(SystemAccount account);
        bool DeleteSystemAccount(SystemAccount account);
    }
}
