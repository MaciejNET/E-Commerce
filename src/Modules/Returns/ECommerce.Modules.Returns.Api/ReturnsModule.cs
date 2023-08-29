using ECommerce.Modules.Returns.Application;
using ECommerce.Modules.Returns.Domain;
using ECommerce.Modules.Returns.Infrastructure;
using ECommerce.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Modules.Returns.Api;

internal class ReturnsModule : IModule
{
    public const string BasePath = "returns-module";
    public string Name { get; } = "Returns";
    public string Path => BasePath;
    public IEnumerable<string> Policies => new[] {"returns"};

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