using FUNewsManagement.BusinessObjects;
using System.Linq.Expressions;

namespace FUNewsManagement.Repositories.IRepositories
{
    public interface INewsArticleRepository
    {
        Task<NewsArticle?> AddAsync(NewsArticle newsArticle);
        Task<NewsArticle?> UpdateAsync(NewsArticle newsArticle);
        Task<NewsArticle?> DeleteAsync(NewsArticle newsArticle);
        Task<IEnumerable<NewsArticle>> GetAllAsync(Expression<Func<NewsArticle, bool>>? condition);
        Task<NewsArticle?> GetAsync(Expression<Func<NewsArticle, bool>> condition);
    }
}
