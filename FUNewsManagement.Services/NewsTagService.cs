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

        private readonly INewsTagRepository _newsTagRepo;
        private readonly INewsArticleRepository _articleRepo;

        // =================================
        // === Constructors
        // =================================

        public NewsTagService(INewsTagRepository newsTagRepo, INewsArticleRepository articleRepo)
        {
            _newsTagRepo = newsTagRepo;
            _articleRepo = articleRepo;
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
                await _newsTagRepo.AddAsync(tag);
            }
        }

        // Existing: A B C D E
        // Select: B C D G H
        // Remove: A E
        // Add New: G H
        // After Add New: B C D G H
        public async Task UpdateNewsTags(ICollection<int> newsTagIdsToAdd, string newsArticleId)
        {
            // Get existing tags
            var existingTagsOfArticle = await _newsTagRepo.GetAllAsync(nt => nt.NewsArticleID == newsArticleId);
            var existingTagIds = existingTagsOfArticle.Select(nt => nt.TagID).ToHashSet();

            // Remove news tags that are not selected from the View
            var newsTagsToRemove = existingTagsOfArticle
                .Where(ex => !newsTagIdsToAdd.Contains(ex.TagID));

            // Add actual new Tag to the news article
            var newsTagsToAdd = newsTagIdsToAdd
                .Except(existingTagIds)
                .Select(id => new NewsTag()
                {
                    NewsArticleID = newsArticleId,
                    TagID = id
                });

            // Remove old tagsNews
            if (newsTagsToRemove.Any())
            {
                await _newsTagRepo.RemoveTagsFromArticle(newsTagsToRemove);
            }

            // Add new tags
            if (newsTagsToAdd.Any())
            {
                await _newsTagRepo.AddTagsToArticle(newsTagsToAdd);
            }
        }
    }
}
