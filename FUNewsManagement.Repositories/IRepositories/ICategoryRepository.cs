using FUNewsManagement.BusinessObjects;
using System.Linq.Expressions;

namespace FUNewsManagement.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<Category?> AddAsync(Category category);
        Task<Category?> UpdateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>>? condition);
        Task<Category?> GetAsync(Expression<Func<Category, bool>> condition);
    }
}
