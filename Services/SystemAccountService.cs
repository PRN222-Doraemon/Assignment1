using BusinessObjects;
using Repositories;
using Repositories.IRepositories;
using Services.IService;

namespace Services
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository _repo;
        public SystemAccountService()
        {
            _repo = new SystemAccountRepository();
        }

        public bool AddSystemAccount(SystemAccount account)
        {
            return _repo.AddSystemAccount(account);
        }

        public SystemAccount CheckLogin(string email, string password)
        {
            return _repo.CheckLogin(email, password);
        }

        public bool DeleteSystemAccount(SystemAccount account)
        {
            return _repo.DeleteSystemAccount(account);
        }

        public SystemAccount GetAccountById(short accountId)
        {
            return _repo.GetAccountById(accountId); 
        }

        public IEnumerable<SystemAccount> GetAllAccounts()
        {
            return _repo.GetAllAccounts();
        }

        public bool UpdateSystemAccount(SystemAccount account)
        {
            return _repo.UpdateSystemAccount(account);
        }
    }
}
