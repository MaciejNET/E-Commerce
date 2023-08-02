using ECommerce.Modules.Reviews.Core;
using ECommerce.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Modules.Reviews.Api;

internal class ReviewsModule : IModule
{
    public const string BasePath = "reviews-module";
    public string Name { get; } = "Reviews";
    public string Path => BasePath;
    public IEnumerable<string> Policies => new[] {"reviews"};

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
    }
}