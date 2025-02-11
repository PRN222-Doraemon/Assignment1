using BusinessObjects;
using DataAccessLayer;
using Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TagRepository : ITagRepository
    {
        public async Task<IEnumerable<Tag>> GetAllTags() => await TagDAO.GetTags();

        public async Task<Tag?> GetTagById(int id) => await TagDAO.GetTagById(id);
    }
}
