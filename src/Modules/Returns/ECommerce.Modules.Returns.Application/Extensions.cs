using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Modules.Returns.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}