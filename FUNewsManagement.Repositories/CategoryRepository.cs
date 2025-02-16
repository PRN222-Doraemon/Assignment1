using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Repositories.Data;
using FUNewsManagement.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FUNewsManagement.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        // =================================
        // === Fields & Props
        // =================================

        private readonly FunewsManagementContext _context;

        // =================================
        // === Constructors
        // =================================

        public CategoryRepository(FunewsManagementContext context)
        {
            _context = context;
        }

        // =================================
        // === Methods
        // =================================

        public async Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>>? condition)
        {
            return await _context.Categories
                .Where(condition ?? (_ => true))
                .ToListAsync();
        }

        public async Task<Category?> GetAsync(Expression<Func<Category, bool>> condition)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(condition);
        }

        public async Task<Category?> AddAsync(Category category)
        {
            var addedEntity = _context.Categories.Add(category).Entity;
            await _context.SaveChangesAsync();
            return addedEntity;
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var updatedCategory = _context.Categories.Update(category).Entity;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updatedCategory;
        }
    }
}
