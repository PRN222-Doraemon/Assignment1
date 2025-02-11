using BusinessObjects;

namespace Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(short id);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category); 
    }
}
