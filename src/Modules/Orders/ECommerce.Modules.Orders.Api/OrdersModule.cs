using ECommerce.Modules.Orders.Application;
using ECommerce.Modules.Orders.Domain;
using ECommerce.Modules.Orders.Infrastructure;
using ECommerce.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Modules.Orders.Api;

internal class OrdersModule : IModule
{
    public const string BasePath = "orders-module";
    public string Name { get; } = "Orders";
    public string Path => BasePath;

    public IEnumerable<string> Policies { get; } = new[]
    {
        "carts", "orders"
    };

    public void Register(IServiceCollection services)
    {
        services
            .AddDomain()
            .AddApplication()
            .AddInfrastructure();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}