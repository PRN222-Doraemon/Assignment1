using BusinessObjects;

namespace Services.IService
{
    public interface ISystemAccountService
    {
        SystemAccount CheckLogin(string email, string password);    
        SystemAccount GetAccountById(short accountId);
        IEnumerable<SystemAccount> GetAllAccounts();
        bool AddSystemAccount(SystemAccount account);
        bool UpdateSystemAccount(SystemAccount account);
        bool DeleteSystemAccount(SystemAccount account);
    }
}
