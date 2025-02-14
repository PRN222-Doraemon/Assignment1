using FUNewsManagement.BusinessObjects;
using FUNewsManagement.Repositories.IRepositories;
using FUNewsManagement.Services.IServices;

namespace FUNewsManagement.Services
{
    public class TagService : ITagService
    {
        // =================================
        // === Fields & Props
        // =================================

        private readonly ITagRepository _repo;

        // =================================
        // === Constructors
        // =================================

        public TagService(ITagRepository repo)
        {
            _repo = repo;
        }

        // =================================
        // === Methods
        // =================================

        public async Task<List<Tag>> GetAllTags(string? searchName = null)
        {
            return (List<Tag>)await _repo
                .GetAllAsync(t => string.IsNullOrEmpty(searchName) || t.TagName!.Contains(searchName));
        }

        public async Task<Tag?> GetTagById(int id)
        {
            return await _repo.GetAsync(t => t.TagId == id);
        }
    }
}
