using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Infrastructure.AzureSqlEdge;

public static class Extensions
{
    internal static IServiceCollection AddAzureSqlEdge(this IServiceCollection services)
    {
        var options = services.GetOptions<SqlServerOptions>("mssql");
        services.AddSingleton(options);

        return services;
    }

    public static IServiceCollection AddAzureSqlEdge<T>(this IServiceCollection services) where T : DbContext
    {
        var options = services.GetOptions<SqlServerOptions>("azureSqlEdge");
        services.AddDbContext<T>(x => x.UseSqlServer(options.ConnectionString));

        return services;
    }
}