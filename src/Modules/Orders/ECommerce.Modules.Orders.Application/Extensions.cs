using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("ECommerce.Modules.Orders.UnitTests")]
[assembly:InternalsVisibleTo("DynamicProxyAssemblyGen2")]
namespace ECommerce.Modules.Orders.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}