using ECommerce.Modules.Returns.Domain.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Modules.Returns.Domain;

public static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddSingleton<IReturnPolicyFactory, ReturnPolicyFactory>();
        return services;
    }
}