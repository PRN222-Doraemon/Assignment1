using BusinessObjects;
using Repositories;
using Repositories.IRepositories;
using Services.IService;

namespace Services
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _repo;
        public NewsArticleService()
        {
            _repo = new NewsArticleRepository(); 
        }
        public async Task AddNewsArticle(NewsArticle p)
        {
            var existingNewsArticle = await _repo.GetNewsArticleById(p.NewsArticleId);
            if (existingNewsArticle != null)
            {
                throw new ArgumentException("This ID has already exist!");
            }
            p.CreatedDate = DateTime.Now;
            p.ModifiedDate = DateTime.Now;
            await _repo.AddNewsArticle(p);
        }

        public async Task DeleteNewsArticle(string id)
        {
            var obj = await _repo.GetNewsArticleById(id);
            if (obj == null)
            {
                throw new Exception($"News arrticle with {id} not found!");
            }
            obj.NewsStatus = false;
            await _repo.UpdateNewsArticle(obj);
        }

        public async Task<List<NewsArticle>> GetNewsArticleByCategoryId(int id)
        {
            return await _repo.GetNewsArticleByCategoryId(id);
        }

        public async Task<NewsArticle?> GetNewsArticleById(string id)
        {
            return await _repo.GetNewsArticleById(id);
        }

        public async Task<List<NewsArticle>> GetNewsArticles()
        {
            return await _repo.NewsArticles();
        }

        public async Task<List<NewsArticle>> GetNewsArticlesByCreatedUserId(short createdById)
        {
            return await _repo.NewsArticlesByCreatedUserId(createdById);
        }

        public async Task UpdateNewsArticle(NewsArticle p)
        {
            p.ModifiedDate = DateTime.Now;
            await _repo.UpdateNewsArticle(p);
        }
    }
}
