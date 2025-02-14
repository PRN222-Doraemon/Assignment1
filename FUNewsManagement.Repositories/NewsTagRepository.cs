using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Repositories.Data;
using FUNewsManagement.Repositories.IRepositories;

namespace FUNewsManagement.Repositories
{
    public class NewsTagRepository : INewsTagRepository
    {
        // =================================
        // === Fields & Props
        // =================================

        private readonly FunewsManagementContext _context;

        // =================================
        // === Constructors
        // =================================

        public NewsTagRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        // =================================
        // === Methods
        // =================================

        public async Task<NewsTag?> AddAsync(NewsTag newsTag)
        {
            var addedNewsTag = _context.NewsTags.Add(newsTag).Entity;
            await _context.SaveChangesAsync();
            return addedNewsTag;
        }
    }
}
