using FUNewsManagement.BusinessObjects;

namespace FUNewsManagement.Services.IServices
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllTags(string? searchName = null);
        Task<Tag?> GetTagById(int id);
    }
}
