using FUNewsManagement.Services;
using FUNewsManagement.Services.IServices;
using Microsoft.Extensions.DependencyInjection;

namespace FUNewsManagement.services.Extensions
{
    public static partial class ServiceCollectionExtension
    {
        public static void AddServicesLayer(this IServiceCollection services)
        {
            // Add Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<INewsArticleService, NewsArticleService>();
            services.AddScoped<ISystemAccountService, SystemAccountService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<INewsTagService, NewsTagService>();
        }
    }
}
