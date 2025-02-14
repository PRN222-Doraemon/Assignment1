using FUNewsManagement.BusinessObjects;

namespace FUNewsManagement.Repositories.IRepositories
{
    public interface INewsTagRepository
    {
        Task<NewsTag?> AddAsync(NewsTag newsTag);
    }
}
