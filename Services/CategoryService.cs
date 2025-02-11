using BusinessObjects;
using Repositories;
using Repositories.IRepositories;
using Services.IService;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly INewsArticleRepository _newsRepo;
        public CategoryService()
        {
            _categoryRepo = new CategoryRepository();
            _newsRepo = new NewsArticleRepository();
        }

        public async Task AddCategory(Category category)
        {
            category.IsActive = true;   
            await _categoryRepo.AddCategory(category);
        }

        public async Task DeleteCategory(short id)
        {
            var obj = await _categoryRepo.GetCategoryById(id);
            if (obj == null)
            {
                throw new Exception($"Category with {id} not found!");
            }
            if ((await _newsRepo.GetNewsArticleByCategoryId(id)).Count > 0)
            {
                throw new Exception($"Cannot delete: The item is linked to one or more news articles");
            }
            obj.IsActive = false;
            await _categoryRepo.UpdateCategory(obj);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _categoryRepo.GetCategories();
        }

        public async Task<Category> GetCategoryById(short id)
        {
            return await _categoryRepo.GetCategoryById(id);
        }

        public async Task UpdateCategory(Category category)
        {
            var obj = await _categoryRepo.GetCategoryById(category.CategoryId);
            if (obj == null)
            {
                throw new Exception($"Category with {category.CategoryId} not found!");
            }
            await _categoryRepo.UpdateCategory(category);
        }
    }
}
