using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class SystemAccountDAO
    {
        public static SystemAccount CheckLogin(string email, string password)
        {
            using var context = new FunewsManagementContext();
            return context.SystemAccounts.FirstOrDefault(p => p.AccountEmail.Equals(email) && p.AccountPassword.Equals(password));
        }

        public static SystemAccount GetAccountById(short accountId)
        {
            using var context = new FunewsManagementContext();
            return context.SystemAccounts.FirstOrDefault(p => p.AccountId.Equals(accountId));
        }

        public static IEnumerable<SystemAccount> GetAllSystemAccounts()
        {
            using var context = new FunewsManagementContext();
            return context.SystemAccounts.ToList();
        }

        public static bool UpdateSystemAccount(SystemAccount account)
        {
            using var context = new FunewsManagementContext();
            context.Entry(account).State = EntityState.Modified;
            return (context.SaveChanges()>0);
        }
        public static bool DeleteSystemAccount(SystemAccount account)
        {
            using var context = new FunewsManagementContext();
            context.Remove(account);
            return (context.SaveChanges() > 0);
        }
    }
}
