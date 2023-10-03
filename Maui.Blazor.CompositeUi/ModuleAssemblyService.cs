using System.Reflection;
using DynamicBlazor.Services;

namespace Maui.Blazor.CompositeUi;

public static class ModuleAssemblyService
{
    /// <summary>
    /// Public list of assemblies for the router to bind to
    /// </summary>
    private static List<Assembly> _moduleAssemblies = new List<Assembly>();
    public static List<Assembly> ModuleAssemblies
    {
        get
        {
            if (_moduleAssemblies.Any())
            {
                return _moduleAssemblies;
            }

            // _moduleAssemblies.Add(Assembly.Load("DynamicBlazor.Services"));

            var assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(a => a.Name!.EndsWith("Module")).ToList();
            foreach (var assemblyName in assemblyNames)
            {
                var assembly = Assembly.Load(assemblyName.Name);
                var customAttributes = assembly.CustomAttributes.ToList();
                var companyAttributeData = customAttributes.SingleOrDefault(data => data.AttributeType.Name == "AssemblyCompanyAttribute");
                if (companyAttributeData is null)
                {
                    continue;
                }

                var company = companyAttributeData.ConstructorArguments.First().Value;
                if (company != null && company.ToString() == "ByBox")
                {
                    _moduleAssemblies.Add(Assembly.Load(assemblyName.Name));
                }
            }

            return _moduleAssemblies;
        }
        set => _moduleAssemblies = value;
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
                    .Where(t => t.IsClass && t.IsPublic && (!t.IsAbstract) 
                                && typeof(IModuleInstaller).IsAssignableFrom(t))
                    .ToArray();

                foreach (var installerType in allInstallers)
                {
                    var remoteDependencyResolver = ServiceHelper.GetService<IRemoteDependencyResolver>();
                    var installer = (IModuleInstaller)Activator.CreateInstance(installerType, remoteDependencyResolver);
                    installer.Install();
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