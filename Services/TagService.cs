using BusinessObjects;
using Repositories;
using Repositories.IRepositories;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repo;
        public TagService()
        {
            _repo = new TagRepository(); 
        }
        public async Task<IEnumerable<Tag>> GetAllTags()
        {
            return await _repo.GetAllTags();
        }

        public async Task<Tag?> GetTagById(int id)
        {
            return await _repo.GetTagById(id);
        }
    }
}
