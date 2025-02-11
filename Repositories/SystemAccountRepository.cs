using BusinessObjects;
using DataAccessLayer;
using Repositories.IRepositories;

namespace Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        public bool AddSystemAccount(SystemAccount account) => SystemAccountDAO.UpdateSystemAccount(account);

        public SystemAccount CheckLogin(string username, string password) => SystemAccountDAO.CheckLogin(username, password);

        public bool DeleteSystemAccount(SystemAccount account) => SystemAccountDAO.DeleteSystemAccount(account);

        public SystemAccount GetAccountById(short accountId) => SystemAccountDAO.GetAccountById(accountId);

        public IEnumerable<SystemAccount> GetAllAccounts() => SystemAccountDAO.GetAllSystemAccounts();

        public bool UpdateSystemAccount(SystemAccount account) => SystemAccountDAO.UpdateSystemAccount(account);
    }
}
