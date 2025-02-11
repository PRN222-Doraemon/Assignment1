using BusinessObjects;
using DataAccessLayer;
using Repositories.IRepositories;

namespace Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        public async Task AddNewsArticle(NewsArticle newsArticle) => await NewsArticleDAO.AddNewsArticle(newsArticle);

        public async Task DeleteNewsArticle(NewsArticle newsArticle) => await NewsArticleDAO.DeleteNewsArticle(newsArticle);

        public async Task<List<NewsArticle>> GetNewsArticleByCategoryId(int id) => await NewsArticleDAO.GetNewsArticleByCategoryId(id);

        public async Task<NewsArticle?> GetNewsArticleById(string id) => await NewsArticleDAO.GetNewsArticleById(id);

        public async Task<List<NewsArticle>> NewsArticles() => await NewsArticleDAO.GetNewsArticles();

        public async Task<List<NewsArticle>> NewsArticlesByCreatedUserId(short createdById) => await NewsArticleDAO.GetNewsArticlesByUserId(createdById);

        public async Task UpdateNewsArticle(NewsArticle newsArticle) => await NewsArticleDAO.UpdateNewsArticle(newsArticle);
    }
}
