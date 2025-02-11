using BusinessObjects;

namespace Repositories.IRepositories
{
    public interface INewsArticleRepository
    {
        Task AddNewsArticle(NewsArticle newsArticle);
        Task UpdateNewsArticle(NewsArticle newsArticle);
        Task DeleteNewsArticle(NewsArticle newsArticle);
        Task<List<NewsArticle>> NewsArticles();
        Task<List<NewsArticle>> NewsArticlesByCreatedUserId(short createdById);
        Task<NewsArticle?> GetNewsArticleById(string id); 
        Task<List<NewsArticle>> GetNewsArticleByCategoryId(int id);
        
    }
}
