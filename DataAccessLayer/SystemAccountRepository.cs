using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Repositories.Data;
using FUNewsManagement.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FUNewsManagement.Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        // =================================
        // === Fields & Props
        // =================================

        private readonly FunewsManagementContext _context;

        // =================================
        // === Constructors
        // =================================

        public SystemAccountRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        // =================================
        // === Methods
        // =================================

        public async Task<SystemAccount?> CheckLoginAsync(string email, string password)
        {

            return await _context.SystemAccounts.FirstOrDefaultAsync(p => p.AccountEmail.Equals(email) && p.AccountPassword.Equals(password));
        }

        public async Task<SystemAccount?> GetAsync(Expression<Func<SystemAccount, bool>> condition)
        {

            return await _context.SystemAccounts
                .FirstOrDefaultAsync(condition);
        }

        public async Task<IEnumerable<SystemAccount>> GetAllAsync(Expression<Func<SystemAccount, bool>>? condition)
        {
            return await _context.SystemAccounts
                .Where(condition ?? (_ => true))
                .ToListAsync();
        }

        public async Task<SystemAccount?> UpdateAsync(SystemAccount account)
        {
            var updatedSystemAccount = _context.SystemAccounts.Update(account).Entity;
            await _context.SaveChangesAsync();
            return updatedSystemAccount;
        }
        public async Task<SystemAccount?> DeleteAsync(SystemAccount account)
        {
            var deletedSystemAccount = _context.Remove(account).Entity;
            await _context.SaveChangesAsync();
            return deletedSystemAccount;
        }

        public async Task<SystemAccount?> AddAsync(SystemAccount account)
        {
            var addedSystemAccount = _context.Add(account).Entity;
            await _context.SaveChangesAsync();
            return addedSystemAccount;
        }
    }
}
