using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Shared.Infrastructure.AzureSqlEdge;

public static class Extensions
{
    internal static IServiceCollection AddSqlServer(this IServiceCollection services)
    {
        var options = services.GetOptions<SqlServer>("mssql");
        services.AddSingleton(options);

        return services;
    }

    public static IServiceCollection AddSqlServer<T>(this IServiceCollection services) where T : DbContext
    {
        var options = services.GetOptions<SqlServer>("sqlServer");
        services.AddDbContext<T>(x => x.UseSqlServer(options.ConnectionString));

        return services;
    }
}