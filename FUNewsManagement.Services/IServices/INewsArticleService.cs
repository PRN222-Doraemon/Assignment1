using FUNewsManagement.BusinessObjects;

namespace FUNewsManagement.Services.IServices
{
    public interface INewsArticleService
    {
        Task<bool> AddNewsArticle(NewsArticle p);
        Task<bool> UpdateNewsArticle(NewsArticle p);
        Task<bool> DeleteNewsArticle(string id);
        Task<List<NewsArticle>> GetNewsArticles(string? searchName = null);
        Task<List<NewsArticle>> GetNewsArticlesByCreatedUserId(short createdById);
        Task<List<NewsArticle>> GetNewsArticleByCategoryId(int id);
        Task<NewsArticle?> GetNewsArticleById(string id);
    }
}
