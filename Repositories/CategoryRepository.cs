using BusinessObjects;
using DataAccessLayer;
using Repositories.IRepositories;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public async Task AddCategory(Category category) => await CategoryDAO.AddCategory(category);    

        public async Task<IEnumerable<Category>> GetCategories() => await CategoryDAO.GetCategories();

        public async Task<Category?> GetCategoryById(short id) => await CategoryDAO.GetCategoryById(id);

        public async Task UpdateCategory(Category category) => await CategoryDAO.UpdateCategory(category);
    }
}
