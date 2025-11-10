using Microsoft.EntityFrameworkCore;
using Restaurant_site.Data;
using Restaurant_site.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IRestaurantService, RestaurantService>();

            return services;
        }
    }
}