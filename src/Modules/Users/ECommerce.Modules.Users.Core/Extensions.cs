using System.Runtime.CompilerServices;
using ECommerce.Modules.Users.Core.DAL;
using ECommerce.Modules.Users.Core.DAL.Repositories;
using ECommerce.Modules.Users.Core.Entities;
using ECommerce.Modules.Users.Core.Repositories;
using ECommerce.Modules.Users.Core.Services;
using ECommerce.Modules.Users.Core.Validators;
using ECommerce.Shared.Infrastructure.AzureSqlEdge;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("ECommerce.Modules.Users.Api")]
[assembly: InternalsVisibleTo("ECommerce.Modules.Users.UnitTests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace ECommerce.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddAzureSqlEdge<UsersDbContext>();
        services.AddSingleton<PasswordValidator>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddTransient<IIdentityService, IdentityService>();
        return services;
    }
}