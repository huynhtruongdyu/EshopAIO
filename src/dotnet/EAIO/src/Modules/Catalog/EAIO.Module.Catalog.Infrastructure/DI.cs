using EAIO.Module.Catalog.Infrastructure.Abstraction;
using EAIO.Module.Catalog.Infrastructure.Contexts;
using EAIO.Module.Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EAIO.Module.Catalog.Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connetionString = configuration.GetConnectionString("Default");
            ArgumentNullException.ThrowIfNullOrWhiteSpace(connetionString);

            services.AddDbContext<CatalogDbContext>(options => options.UseInMemoryDatabase(connetionString));
            //services.AddDbContext<CatalogDbContext>(options => options.UseSqlite(connetionString));
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
