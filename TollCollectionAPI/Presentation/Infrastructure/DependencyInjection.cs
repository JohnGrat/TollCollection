using Business.Repositories;
using Business.Repositories.Defaults;
using Data;
using Microsoft.EntityFrameworkCore;
using Presentation.Configs;

namespace Presentation.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TollCollectionDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString(AppConfig.TollCollectionDatabase)));

            services.AddScoped<ITollRepository, TollRepository>();

            return services;
        }
    }
}
