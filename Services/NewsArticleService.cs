using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Repositories.IRepositories;
using FUNewsManagement.Services.IServices;

namespace FUNewsManagement.Services
{
    public class NewsArticleService : INewsArticleService
    {
        // =================================
        // === Fields & Props
        // =================================

        private readonly INewsArticleRepository _newsRepo;

        // =================================
        // === Constructors
        // =================================

        public NewsArticleService(INewsArticleRepository newsRepo)
        {
            _newsRepo = newsRepo;
        }

        // =================================
        // === Methods
        // =================================

        public async Task<bool> AddNewsArticle(NewsArticle p)
        {
            var existingNewsArticle = await _newsRepo.GetAsync(n => n.NewsArticleId == p.NewsArticleId);
            if (existingNewsArticle != null)
            {
                throw new ArgumentException("This ID has already exist!");
            }
            p.CreatedDate = DateTime.Now;
            p.ModifiedDate = DateTime.Now;
            return await _newsRepo.AddAsync(p) != null;
        }

        public async Task<bool> DeleteNewsArticle(string id)
        {
            var obj = await _newsRepo.GetAsync(o => o.NewsArticleId == id);
            if (obj == null)
            {
                throw new Exception($"News article with {id} not found!");
            }
            obj.NewsStatus = false;
            return await _newsRepo.UpdateAsync(obj) != null;
        }

        public async Task<List<NewsArticle>> GetNewsArticleByCategoryId(int id)
        {
            return (List<NewsArticle>)await _newsRepo.GetAllAsync(n => n.CategoryId == id);
        }

        public async Task<NewsArticle?> GetNewsArticleById(string id)
        {
            return await _newsRepo.GetAsync(n => n.NewsArticleId == id);
        }

        public async Task<List<NewsArticle>> GetNewsArticles(string? searchName = null)
        {
            return (List<NewsArticle>)await _newsRepo
                .GetAllAsync(n => string.IsNullOrEmpty(searchName) || n.NewsTitle!.Contains(searchName));
        }

        public async Task<List<NewsArticle>> GetNewsArticlesByCreatedUserId(short createdById)
        {
            return (List<NewsArticle>)await _newsRepo.GetAllAsync(n => n.CreatedById == createdById);
        }

        public async Task<bool> UpdateNewsArticle(NewsArticle p)
        {
            p.ModifiedDate = DateTime.Now;
            return await _newsRepo.UpdateAsync(p) != null;
        }
    }
}
