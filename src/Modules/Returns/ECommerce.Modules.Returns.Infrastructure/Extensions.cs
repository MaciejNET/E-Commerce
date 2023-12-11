using ECommerce.Modules.Returns.Domain.Repositories;
using ECommerce.Modules.Returns.Infrastructure.EF;
using ECommerce.Modules.Returns.Infrastructure.EF.Repositories;
using ECommerce.Shared.Infrastructure.AzureSqlEdge;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Modules.Returns.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSqlServer<ReturnsDbContext>();
        services.AddScoped<IReturnRepository, ReturnRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderProductRepository, OrderProductRepository>();
        return services;
    }
}