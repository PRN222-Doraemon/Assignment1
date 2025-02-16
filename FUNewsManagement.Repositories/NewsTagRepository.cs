using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Repositories.Data;
using FUNewsManagement.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<NewsTag>> GetAllAsync(Expression<Func<NewsTag, bool>>? condition)
        {
            return await _context.NewsTags
                .Where(condition ?? (_ => true))
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task RemoveTagsFromArticle(IEnumerable<NewsTag> newsTagsToRemove)
        {
            _context.NewsTags.RemoveRange(newsTagsToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task AddTagsToArticle(IEnumerable<NewsTag> newsTagsToAdd)
        {
            await _context.NewsTags.AddRangeAsync(newsTagsToAdd);
            await _context.SaveChangesAsync();
        }
    }
}
