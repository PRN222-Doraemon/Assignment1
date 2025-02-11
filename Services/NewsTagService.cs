using BusinessObjects;
using Repositories;
using Repositories.IRepositories;
using Services.IService;

namespace Services
{
    public class NewsTagService : INewsTagService
    {
        private readonly INewsTagRepository _repo;
        public NewsTagService()
        {
            _repo = new NewsTagRepository();
        }
        public async Task AddNewsTag(ICollection<int> newsTagIds, string newsArticleId)
        {
            foreach (int tagId in newsTagIds)
            {
                NewsTag tag = new()
                {
                    NewsArticleID = newsArticleId,
                    TagID = tagId
                };
                await _repo.AddNewsTag(tag);
            }           
        }
    }
}
