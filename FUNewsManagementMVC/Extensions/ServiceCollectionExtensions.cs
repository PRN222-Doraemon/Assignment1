﻿using Syncfusion.Licensing;

namespace FUNewsManagementMVC.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Add Session
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20); //Set session timeout
                options.Cookie.HttpOnly = true; // For sercurity
                options.Cookie.IsEssential = true; // Ensure session cookie is always created
            });

            // Add Syncfusion
            SyncfusionLicenseProvider.RegisterLicense(configuration["Syncfusion:LicenseKey"]);
        }
    }
}
