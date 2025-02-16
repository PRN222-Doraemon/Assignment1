using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Repositories.IRepositories;
using FUNewsManagement.Services.IServices;

namespace FUNewsManagement.Services
{
    public class CategoryService : ICategoryService
    {
        // =================================
        // === Fields & Props
        // =================================

        private readonly ICategoryRepository _categoryRepo;
        private readonly INewsArticleRepository _newsRepo;

        // =================================
        // === Constructors
        // =================================

        public CategoryService(ICategoryRepository categoryRepo, INewsArticleRepository newsRepo)
        {
            _categoryRepo = categoryRepo;
            _newsRepo = newsRepo;
        }

        // =================================
        // === Methods
        // =================================

        public async Task<bool> AddCategory(Category category)
        {
            category.IsActive = true;
            return await _categoryRepo.AddAsync(category) != null;
        }

        public async Task<bool> DeleteCategory(short id)
        {
            var category = await _categoryRepo.GetAsync(c => c.CategoryId == id)
                ?? throw new Exception($"Category with {id} not found!");

            if (await _newsRepo.GetAsync(n => n.CategoryId == id) != null)
            {
                throw new Exception($"Cannot delete: The category is linked to one or more news articles");
            }

            category.IsActive = false;
            return await _categoryRepo.UpdateAsync(category) != null;
        }

        public async Task<List<Category>> GetCategories(string? searchName = null)
        {
            return (List<Category>)await _categoryRepo.GetAllAsync(c => string.IsNullOrEmpty(searchName) || c.CategoryName.Contains(searchName));
        }

        public async Task<Category> GetCategoryById(short id)
        {
            return await _categoryRepo.GetAsync(c => c.CategoryId == id)
                ?? throw new Exception($"Category with {id} not found!");
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            var existingCategory = await _categoryRepo.GetAsync(c => c.CategoryId == category.CategoryId)
                ?? throw new Exception($"Category with {category.CategoryId} not found!");

            existingCategory.CategoryName = category.CategoryName;
            existingCategory.CategoryDesciption = category.CategoryDesciption;
            existingCategory.ParentCategoryId = category.ParentCategoryId;
            existingCategory.IsActive = category.IsActive;

            return await _categoryRepo.UpdateAsync(existingCategory) != null;
        }
    }
}
