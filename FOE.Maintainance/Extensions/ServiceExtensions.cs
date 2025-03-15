using Contracts.Base;
using Repository.Repository;
using Service.Services;
using Core.LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
namespace FOE.Maintainance.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureLogger(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            return services;
        }
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfigurationRoot config)
        {
            services.AddDbContext<FoeMaintainContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("FoeMaintainanceDB"), o => o.MigrationsAssembly("FOE.Maintainance"));
            });
            return services;
        }
        public static IServiceCollection ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            return services;
        }
        public static IServiceCollection ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
