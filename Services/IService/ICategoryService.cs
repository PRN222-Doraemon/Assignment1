using BusinessObjects;

namespace Services.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategoryById(short id);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(short id);    
    }
}
