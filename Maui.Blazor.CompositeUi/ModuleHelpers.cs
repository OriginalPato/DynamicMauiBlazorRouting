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
        var allInstallers = AppDomain.CurrentDomain.GetAssemblies()                                    
            .Where(x => x.FullName.Contains("Module"))
            .Concat(ModuleAssemblyService.ModuleAssemblies)  //Only needed for runtime composition
            .Distinct()
            .SelectMany(s => s.GetExportedTypes())
            .Where(t => t.IsClass && t.IsPublic && (!t.IsAbstract) 
                        && typeof(IModuleInstaller).IsAssignableFrom(t))
            .ToArray();

        foreach (var installerType in allInstallers)
        {
            var installer = (IModuleInstaller)Activator.CreateInstance(installerType);
            installer.Install(builder.Services);
        }
        builder.Services.AddSingleton<IRemoteDependencyResolver, RemoteDependencyResolver>();
        // builder.Services.AddSingleton<IModuleAssemblyService, ModuleAssemblyService>();
        return builder;
    }
}