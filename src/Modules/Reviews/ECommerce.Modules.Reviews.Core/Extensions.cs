using System.Runtime.CompilerServices;
using ECommerce.Modules.Reviews.Core.DAL;
using ECommerce.Modules.Reviews.Core.DAL.Repositories;
using ECommerce.Modules.Reviews.Core.Repositories;
using ECommerce.Modules.Reviews.Core.Services;
using ECommerce.Shared.Infrastructure.AzureSqlEdge;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ECommerce.Modules.Reviews.Api")]
[assembly: InternalsVisibleTo("ECommerce.Modules.Reviews.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace ECommerce.Modules.Reviews.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSqlServer<ReviewsDbContext>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewService, ReviewService>();
        
        return services;
    }
}