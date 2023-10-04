using System.Reflection;

namespace Maui.Blazor.CompositeUi;

public static class ModuleAssemblyService
{
    /// <summary>
    /// Public list of assemblies for the router to bind to
    /// </summary>
    private static List<Assembly> _moduleAssemblies = new();
    public static List<Assembly> ModuleAssemblies
    {
        get
        {
            if (_moduleAssemblies.Any())
            {
                return _moduleAssemblies;
            }

            var assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(a => a.Name!.EndsWith("Module")).ToList();
            foreach (var assemblyName in assemblyNames)
            {
                var assembly = Assembly.Load(assemblyName.Name ?? throw new InvalidOperationException());
                var customAttributes = assembly.CustomAttributes.ToList();
                var companyAttributeData = customAttributes.SingleOrDefault(data => data.AttributeType.Name == "AssemblyCompanyAttribute");

                var company = companyAttributeData?.ConstructorArguments.First().Value;
                
                //TODO add company flag to make sure you are only loading DLL's from the company you want. 
                _moduleAssemblies.Add(Assembly.Load(assemblyName.Name));
            }

            return _moduleAssemblies;
        }
        private set => _moduleAssemblies = value;
    }

    /// <summary>
    /// Load assemblies from the list of modules
    /// </summary>
    /// <param name="modules"></param>
    /// <returns></returns>
    public static async Task<bool> GetAssemblyFromRemoteSource(List<Module> modules)
    {
        using var client = new HttpClient();
        var errors = 0;
        foreach (var module in modules)
        {
            var res = await client.GetByteArrayAsync(
                module.DownloadUrl);
            try
            {
                var assembly = Assembly.Load(res);
                var allInstallers = assembly.GetExportedTypes()
                    .Where(t => t is { IsClass: true, IsPublic: true, IsAbstract: false }
                                && typeof(IModuleInstaller).IsAssignableFrom(t))
                    .ToArray();

                foreach (var installerType in allInstallers)
                {
                    var remoteDependencyResolver = ServiceHelper.GetService<IRemoteDependencyResolver>();
                    var installer = Activator.CreateInstance(installerType, remoteDependencyResolver) as IModuleInstaller;
                    installer?.Install();
                }
                ModuleAssemblies = ModuleAssemblies.Append(assembly).ToList();
            }
            catch (Exception)
            {
                // Throw if we can't load the assembly and return false 
                errors++;
            }
        }
    
        return errors == 0;
    }
}