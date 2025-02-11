
using BusinessObjects;
using DataAccessLayer;
using Repositories.IRepositories;

namespace Repositories
{
    public class NewsTagRepository : INewsTagRepository
    {
        public async Task AddNewsTag(NewsTag newsTag) => await NewsTagDAO.AddNewsTag(newsTag);
    }
}
