using BusinessObjects;

namespace DataAccessLayer
{
    public class NewsTagDAO
    {
        public async static Task AddNewsTag(NewsTag newsTag)
        {
            var context = new FunewsManagementContext();
            context.NewsTags.Add(newsTag);
            await context.SaveChangesAsync();
        }
    }
}
