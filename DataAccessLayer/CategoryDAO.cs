using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        public async static Task<IEnumerable<Category>> GetCategories()
        {
            var listCategories = new List<Category>();
            var context = new FunewsManagementContext();
            listCategories = await context.Categories.ToListAsync();
            return listCategories;
        }

        public async static Task<Category?> GetCategoryById(short id)
        {
            var context = new FunewsManagementContext();
            var category = await context.Categories.FirstOrDefaultAsync(p => p.CategoryId == id);
            return category;
        }

        public async static Task AddCategory(Category category)
        {
            var context = new FunewsManagementContext();
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        public async static Task UpdateCategory(Category category)
        {
            var context = new FunewsManagementContext();
            context.Entry(category).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
