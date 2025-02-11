using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class NewsArticleDAO
    {
        public async static Task<List<NewsArticle>> GetNewsArticles()
        {
            var listProducts = new List<NewsArticle>();
            var db = new FunewsManagementContext();
            listProducts = await db.NewsArticles
                    .Include(f => f.Category)
                    .Include(f => f.CreatedBy)
                    .Include(f => f.NewsTags).ThenInclude(p=>p.Tag)
                    .Where(f=>f.NewsStatus == true).ToListAsync();
            return listProducts;
        }
        public async static Task<List<NewsArticle>> GetNewsArticlesByUserId(short createdById)
        {
            var listProducts = new List<NewsArticle>();
            var db = new FunewsManagementContext();
            listProducts = await db.NewsArticles
                    .Include(f => f.Category)
                    .Include(f => f.CreatedBy)
                    .Include(f => f.NewsTags).ThenInclude(p => p.Tag)
                    .Where(f => f.NewsStatus == true && f.CreatedById == createdById)
                    .ToListAsync();
            return listProducts;
        }

        public async static Task AddNewsArticle(NewsArticle newsArticle)
        {
            var context = new FunewsManagementContext();
            context.NewsArticles.Add(newsArticle);
            await context.SaveChangesAsync();
        }
        public async static Task UpdateNewsArticle(NewsArticle newsArticle)
        {
            var context = new FunewsManagementContext();
            context.Entry(newsArticle).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async static Task DeleteNewsArticle(NewsArticle newsArticle)
        {
            var context = new FunewsManagementContext();
            context.NewsArticles.Remove(newsArticle);
            await context.SaveChangesAsync();
        }
        public async static Task<NewsArticle?> GetNewsArticleById(string id)
        {
            var context = new FunewsManagementContext();
            return await context.NewsArticles
                .Include(p => p.Category)
                .Include(p => p.CreatedBy)
                .Include(p => p.NewsTags).ThenInclude(p => p.Tag)
                .FirstOrDefaultAsync(p => p.NewsArticleId == id);
        }
        public async static Task<List<NewsArticle>> GetNewsArticleByCategoryId(int id)
        {
            var context = new FunewsManagementContext();
            return await context.NewsArticles
                .Include(p => p.Category)
                .Include(p => p.CreatedBy)
                .Include(p => p.NewsTags).ThenInclude(p => p.Tag)
                .Where(p => p.CategoryId == id)
                .ToListAsync();
        }
    }
}
