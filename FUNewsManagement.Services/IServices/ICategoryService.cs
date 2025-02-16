using FUNewsManagement.BusinessObjects;

namespace FUNewsManagement.Services.IServices
{
    public interface ICategoryService
    {
        Task<bool> AddCategory(Category category);
        Task<bool> UpdateCategory(Category category);
        Task<bool> DeleteCategory(short id);
        Task<List<Category>> GetCategories(string? searchName = null);
        Task<Category> GetCategoryById(short id);
    }
}
