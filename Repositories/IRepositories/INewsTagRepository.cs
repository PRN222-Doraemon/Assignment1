

using BusinessObjects;

namespace Repositories.IRepositories
{
    public interface INewsTagRepository
    {
        Task AddNewsTag(NewsTag newsTag);
    }
}
