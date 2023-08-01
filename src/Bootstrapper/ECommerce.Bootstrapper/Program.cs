using ECommerce.Bootstrapper;
using ECommerce.Shared.Infrastructure;
using ECommerce.Shared.Infrastructure.Modules;
using Microsoft.Identity.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureModules();

var assemblies = ModuleLoader.LoadAssemblies(builder.Configuration);
var modules = ModuleLoader.LoadModules(assemblies);

builder.Services.AddInfrastructure(assemblies, modules);

foreach (var module in modules)
{
    module.Register(builder.Services);
}

var app = builder.Build();

app.Logger.LogInformation("Modules: {Module}", string.Join(", ", modules.Select(x => x.Name)));
app.UseInfrastructure();

foreach (var module in modules)
{
    module.Use(app);
}

app.MapGet("/", () => "ECommerce API");
app.MapModuleInfo();

assemblies.Clear();
modules.Clear();

app.Run();