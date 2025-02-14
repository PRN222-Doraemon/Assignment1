using FUNewsManagement.BusinessObjects;
using System.Linq.Expressions;

namespace FUNewsManagement.Repositories.IRepositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync(Expression<Func<Tag, bool>>? condition);
        Task<Tag?> GetAsync(Expression<Func<Tag, bool>> condition);
    }
}
