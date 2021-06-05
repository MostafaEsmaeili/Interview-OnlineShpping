using Microsoft.Extensions.DependencyInjection;
using Catalog.Infrastructure.Persistence.EntityFramework;
using Catalog.Infrastructure.Services;
using Catalog.Application.Common.interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Catalog.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContextPool<ICatalogDbContext,CatalogDbContext>((serviceProvider,options) =>
            {
                var configurations = serviceProvider.GetRequiredService<IConfiguration>();
                options.UseSqlServer(configurations.GetConnectionString(nameof(CatalogDbContext)), x => x.EnableRetryOnFailure(3));

                #if DEBUG
                options.UseLoggerFactory(LoggerFactory.Create(c => c.AddConsole())).EnableSensitiveDataLogging();
                #endif
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddHttpContextAccessor();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IDateTime, DateTimeService>();

            return services;

        }
    }
}
