namespace FUNewsManagement.Services.IServices
{
    public interface INewsTagService
    {
        Task AddNewsTag(ICollection<int> newsTagIds, string newsArticleId);
    }
}
