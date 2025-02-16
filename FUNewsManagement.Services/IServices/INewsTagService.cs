namespace FUNewsManagement.Services.IServices
{
    public interface INewsTagService
    {
        Task AddNewsTag(ICollection<int> newsTagIds, string newsArticleId);
        Task UpdateNewsTags(ICollection<int> newsTagIds, string newsArticleId);
    }
}
