using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Hosting;

namespace Maui.Blazor.CompositeUi;

public static class ModuleHelpers
{
    /// <summary>
    /// Helper method to register services to load and register services from modules that will be loaded at runtime.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static MauiAppBuilder RegisterModuleServices(
        this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<IRemoteDependencyResolver, RemoteDependencyResolver>();
        builder.Services.AddSingleton<IModuleAssemblyService, ModuleAssemblyService>();
        return builder;
    }
}