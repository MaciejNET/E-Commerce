using System.Runtime.CompilerServices;
using ECommerce.Modules.Products.Core.DAL;
using ECommerce.Modules.Products.Core.DAL.Repositories;
using ECommerce.Modules.Products.Core.Repositories;
using ECommerce.Modules.Products.Core.Services;
using ECommerce.Shared.Infrastructure.AzureSqlEdge;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ECommerce.Modules.Products.Api")]
[assembly: InternalsVisibleTo("ECommerce.Modules.Products.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace ECommerce.Modules.Products.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSqlServer<ProductsDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}