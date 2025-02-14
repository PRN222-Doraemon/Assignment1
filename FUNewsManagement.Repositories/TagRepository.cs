using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Repositories.Data;
using FUNewsManagement.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FUNewsManagement.Repositories
{
    public class TagRepository : ITagRepository
    {
        // =================================
        // === Fields & Props
        // =================================

        private readonly FunewsManagementContext _context;

        // =================================
        // === Constructors
        // =================================

        public TagRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        // =================================
        // === Methods
        // =================================

        public async Task<Tag?> GetAsync(Expression<Func<Tag, bool>> condition)
        {
            return await _context.Tags
                .Include(p => p.NewsTags)
                .AsNoTracking()
                .FirstOrDefaultAsync(condition);
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(Expression<Func<Tag, bool>>? condition)
        {
            return await _context.Tags
                .Where(condition ?? (_ => true))
                .ToListAsync();
        }
    }
}
