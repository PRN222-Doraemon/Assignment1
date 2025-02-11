using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class TagDAO
    {
        public async static Task<IEnumerable<Tag>> GetTags()
        {
            var listTags = new List<Tag>();
            var context = new FunewsManagementContext();
            listTags = await context.Tags.ToListAsync();
            return listTags;
        }

        public async static Task<Tag?> GetTagById(int id)
        {
            var context = new FunewsManagementContext();
            var tag = await context.Tags
                .Include(p => p.NewsTags)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.TagId == id);            
            return tag;
        }
    }
}
