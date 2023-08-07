using ECommerce.Modules.Discounts.Core;
using ECommerce.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Modules.Discounts.Api;

internal class DiscountsModule : IModule
{
    public const string BasePath =  "discounts-module";
    public string Name { get; } = "Discounts";
    public string Path => BasePath;
    public IEnumerable<string> Policies => new[] {"discounts"};

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}