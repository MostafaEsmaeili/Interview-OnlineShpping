using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Order.Infrastructure.Persistence.EntityFramework;
using Order.Infrastructure.Services;
using Order.Application.Common.interfaces;

namespace Order.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContextPool<IOrderDbContext, OrderDbContext>((serviceProvider, options) =>
             {
                 var configurations = serviceProvider.GetRequiredService<IConfiguration>();
                 options.UseSqlServer(configurations.GetConnectionString(nameof(OrderDbContext)), x => x.EnableRetryOnFailure(3));

#if DEBUG
                 options.UseLoggerFactory(LoggerFactory.Create(c => c.AddConsole())).EnableSensitiveDataLogging();
#endif
             });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddHttpContextAccessor();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<ICatalogMicroService, CatalogMicroService>();
            services.AddHttpClient();
            return services;

        }
    }
}
