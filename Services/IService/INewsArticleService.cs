using BusinessObjects;

namespace Services.IService
{
    public interface INewsArticleService
    {
        Task AddNewsArticle(NewsArticle p);
        Task UpdateNewsArticle(NewsArticle p);
        Task DeleteNewsArticle(string id);
        Task<List<NewsArticle>> GetNewsArticles();
        Task<List<NewsArticle>> GetNewsArticlesByCreatedUserId(short createdById);
        Task<NewsArticle?> GetNewsArticleById(string id);
        Task<List<NewsArticle>> GetNewsArticleByCategoryId(int id);
    }
}
