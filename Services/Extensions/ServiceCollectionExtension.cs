using FUNewsManagement.Repositories.Data;
using FUNewsManagement.Repositories.IRepositories;
using FUNewsManagement.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUNewsManagement.Services.IServices;
using FUNewsManagement.Services;

namespace FUNewsManagement.services.Extensions
{
    public static class ServiceCollectionExtension
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
