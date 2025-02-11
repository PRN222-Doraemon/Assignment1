using BusinessObjects;

namespace Services.IService
{
    public interface INewsTagService
    {
        Task AddNewsTag(ICollection<int> newsTagIds, string newsArticleId);
    }
}
