using System.Runtime.CompilerServices;
using ECommerce.Modules.Discounts.Core.BackgroundServices;
using ECommerce.Modules.Discounts.Core.DAL;
using ECommerce.Modules.Discounts.Core.DAL.Repositories;
using ECommerce.Modules.Discounts.Core.Repositories;
using ECommerce.Modules.Discounts.Core.Services;
using ECommerce.Modules.Discounts.Core.Validators;
using ECommerce.Shared.Infrastructure.AzureSqlEdge;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ECommerce.Modules.Discounts.Api")]
[assembly: InternalsVisibleTo("ECommerce.Modules.Discounts.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace ECommerce.Modules.Discounts.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<DiscountDateValidator>();
        services.AddAzureSqlEdge<DiscountsDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IDiscountCodeRepository, DiscountCodeRepository>();
        services.AddScoped<IProductDiscountRepository, ProductDiscountRepository>();
        services.AddScoped<IProductDiscountService, ProductDiscountService>();
        services.AddScoped<IDiscountCodeService, DiscountCodeService>();
        services.AddHostedService<DiscountCodeExpirationService>();
        services.AddHostedService<ProductDiscountExpirationService>();
        
        return services;
    }
}