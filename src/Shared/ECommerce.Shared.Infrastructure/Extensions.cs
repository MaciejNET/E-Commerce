using System.Reflection;
using System.Runtime.CompilerServices;
using ECommerce.Shared.Abstractions.Modules;
using ECommerce.Shared.Abstractions.Time;
using ECommerce.Shared.Infrastructure.Api;
using ECommerce.Shared.Infrastructure.AzureSqlEdge;
using ECommerce.Shared.Infrastructure.Events;
using ECommerce.Shared.Infrastructure.Exceptions;
using ECommerce.Shared.Infrastructure.Messaging;
using ECommerce.Shared.Infrastructure.Modules;
using ECommerce.Shared.Infrastructure.Services;
using ECommerce.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

[assembly: InternalsVisibleTo("ECommerce.Bootstrapper")]
namespace ECommerce.Shared.Infrastructure;

internal static class Extensions
{
    private const string CorsPolicy = "cors";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IList<Assembly> assemblies, IList<IModule> modules)
    {
        var disabledModules = new List<string>();
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        foreach (var (key, value) in configuration.AsEnumerable())
        {
            if (!key.Contains(":module:enabled"))
            {
                continue;
            }

            if (!bool.Parse(value))
            {
                disabledModules.Add(key.Split(":")[0]);
            }
        }

        services.AddCors(cors =>
        {
            cors.AddPolicy(CorsPolicy, x =>
            {
                x.WithOrigins("*")
                    .WithMethods("POST", "PUT", "DELETE")
                    .WithHeaders("Content-Type", "Authorization");
            });
        });

        services.AddSwaggerGen(swagger =>
        {
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "ECommerce API",
                Version = "v1"
            });
        });

        services.AddModuleInfo(modules);
        services.AddModuleRequests(assemblies);
        services.AddErrorHandling();
        services.AddEvents(assemblies);
        services.AddMessaging();
        services.AddAzureSqlEdge();
        services.AddSingleton<IClock, UtcClock>();
        services.AddHostedService<AppInitializer>();
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
                        StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                {
                    manager.ApplicationParts.Remove(part);
                }
                
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });
        services.AddEndpointsApiExplorer();
        
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseCors(CorsPolicy);
        app.UseErrorHandling();
        app.UseSwagger();
        app.UseSwaggerUI(swagger =>
        {
            swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API V1");
            swagger.DocumentTitle = "ECommerce API";
        });
        app.MapControllers();
        app.UseRouting();
        return app;
    }
    
    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
}