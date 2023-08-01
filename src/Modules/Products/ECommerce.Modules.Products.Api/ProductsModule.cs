using ECommerce.Modules.Products.Core;
using ECommerce.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Modules.Products.Api;

internal class ProductsModule : IModule
{
    public const string BasePath = "products-module";
    public string Name { get; } = "Products";
    public string Path => BasePath;
    public IEnumerable<string> Policies => new[] {"products", "category"};

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}