using System.Reflection;
using ECommerce.Shared.Abstractions.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Infrastructure.Kernel;

internal static class Extensions
{
    public static IServiceCollection AddDomainEvents(this IServiceCollection services,
        IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}