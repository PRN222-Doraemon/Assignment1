using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Repositories.Data;
using FUNewsManagement.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FUNewsManagement.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        // =================================
        // === Fields & Props
        // =================================

        private readonly FunewsManagementContext _context;

        // =================================
        // === Constructors
        // =================================

        public NewsArticleRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        // =================================
        // === Methods
        // =================================

        public async Task<IEnumerable<NewsArticle>> GetAllAsync(
            Expression<Func<NewsArticle, bool>>? condition = null,
            Expression<Func<NewsArticle, object>>? orderByAsc = null,
            Expression<Func<NewsArticle, object>>? orderByDesc = null)
        {
            IQueryable<NewsArticle> query = _context.NewsArticles
                    .Include(f => f.Category)
                    .Include(f => f.CreatedBy)
                    .Include(f => f.NewsTags).ThenInclude(p => p.Tag)
                    .Where(condition ?? (_ => true))
                    .Where(f => f.NewsStatus == true);

            if (orderByAsc != null)
            {
                query = query.OrderBy(orderByAsc);
            }
            else if (orderByDesc != null)
            {
                query = query.OrderByDescending(orderByDesc);
            }

            return await query.ToListAsync();
        }

        public async Task<NewsArticle?> GetAsync(Expression<Func<NewsArticle, bool>> condition)
        {
            return await _context.NewsArticles
                .Include(p => p.Category)
                .Include(p => p.CreatedBy)
                .Include(p => p.NewsTags).ThenInclude(p => p.Tag)
                .FirstOrDefaultAsync(condition);
        }

        public async Task<NewsArticle?> AddAsync(NewsArticle newsArticle)
        {
            var addedVewsArticle = _context.NewsArticles.Add(newsArticle).Entity;
            await _context.SaveChangesAsync();
            return addedVewsArticle;
        }

        public async Task<NewsArticle?> UpdateAsync(NewsArticle newsArticle)
        {
            var updatedNewsArticle = _context.NewsArticles.Update(newsArticle).Entity;
            await _context.SaveChangesAsync();
            return updatedNewsArticle;
        }

        public async Task<NewsArticle?> DeleteAsync(NewsArticle newsArticle)
        {
            var deletedEntity = _context.NewsArticles.Remove(newsArticle).Entity;
            await _context.SaveChangesAsync();
            return deletedEntity;
        }
    }
}
