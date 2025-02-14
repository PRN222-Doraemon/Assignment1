using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Repositories.IRepositories;
using FUNewsManagement.Services.IServices;

namespace FUNewsManagement.Services
{
    public class NewsTagService : INewsTagService
    {
        // =================================
        // === Fields & Props
        // =================================

        private readonly INewsTagRepository _repo;

        // =================================
        // === Constructors
        // =================================

        public NewsTagService(INewsTagRepository repo)
        {
            _repo = repo;
        }

        // =================================
        // === Methods
        // =================================

        public async Task AddNewsTag(ICollection<int> newsTagIds, string newsArticleId)
        {
            foreach (int tagId in newsTagIds)
            {
                NewsTag tag = new()
                {
                    NewsArticleID = newsArticleId,
                    TagID = tagId
                };
                await _repo.AddAsync(tag);
            }
        }
    }
}
