using FUNewsManagement.Repositories.Data;
using FUNewsManagement.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FUNewsManagement.Repositories.Extensions
{
    public static partial class ServiceCollectionExtension
    {
        public static void AddRepositoriesLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // AddAsync db context
            services.AddDbContextPool<FunewsManagementContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
            });

            // Add Repos
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<INewsArticleRepository, NewsArticleRepository>();
            services.AddScoped<INewsTagRepository, NewsTagRepository>();
            services.AddScoped<ISystemAccountRepository, SystemAccountRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
        }
    }
}
