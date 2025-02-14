using FUNewsManagement.BusinessObjects;
using System.Linq.Expressions;

namespace FUNewsManagement.Repositories.IRepositories
{
    public interface ISystemAccountRepository
    {
        Task<SystemAccount?> CheckLoginAsync(string username, string password);
        Task<SystemAccount?> GetAsync(Expression<Func<SystemAccount, bool>> condition);
        Task<IEnumerable<SystemAccount>> GetAllAsync(Expression<Func<SystemAccount, bool>>? condition);
        Task<SystemAccount?> AddAsync(SystemAccount account);
        Task<SystemAccount?> UpdateAsync(SystemAccount account);
        Task<SystemAccount?> DeleteAsync(SystemAccount account);
    }
}
