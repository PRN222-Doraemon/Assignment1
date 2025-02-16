using FUNewsManagement.BusinessObjects;
using System.Linq.Expressions;

namespace FUNewsManagement.Repositories.IRepositories
{
    public interface INewsTagRepository
    {
        Task<NewsTag?> AddAsync(NewsTag newsTag);
        Task<IEnumerable<NewsTag>> GetAllAsync(Expression<Func<NewsTag, bool>>? condition);
        Task RemoveTagsFromArticle(IEnumerable<NewsTag> newsTagsToRemove);
        Task AddTagsToArticle(IEnumerable<NewsTag> newsTagsToAdd);
    }
}
