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
        var allInstallers = AppDomain.CurrentDomain.GetAssemblies()                                    
            .Where(x => x.FullName.Contains("Module"))
            .Concat(ModuleAssemblyService.ModuleAssemblies)  //Only needed for runtime composition
            .Distinct()
            .SelectMany(s => s.GetExportedTypes())
            .Where(t => t is { IsClass: true, IsPublic: true, IsAbstract: false }
                        && typeof(IModuleInstaller).IsAssignableFrom(t))
            .ToArray();
        
        foreach (var installerType in allInstallers)
        {
            var remoteDependencyResolver = ServiceHelper.GetService<IRemoteDependencyResolver>();
            var installer = Activator.CreateInstance(installerType, remoteDependencyResolver) as IModuleInstaller;
            installer?.Install();
        }
        
        return builder;
    }
}