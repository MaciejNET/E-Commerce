using ECommerce.Modules.Orders.Domain.Carts.Repositories;
using ECommerce.Modules.Orders.Domain.Orders.Repositories;
using ECommerce.Modules.Orders.Infrastructure.EF;
using ECommerce.Modules.Orders.Infrastructure.EF.Repositories;
using ECommerce.Shared.Infrastructure.AzureSqlEdge;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Modules.Orders.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAzureSqlEdge<OrdersDbContext>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICheckoutCartRepository, CheckoutCartRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}