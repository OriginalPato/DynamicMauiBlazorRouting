using System.Reflection;

namespace Maui.Blazor.CompositeUi;

public interface IModuleAssemblyService
{
    IEnumerable<Assembly> Assemblies { get; }
    Task<bool> GetAssemblies(List<Module> modules);
}

public class ModuleAssemblyService : IModuleAssemblyService
{
    /// <summary>
    /// Public list of assemblies for the router to bind to
    /// </summary>
    public IEnumerable<Assembly> Assemblies { get; private set; }

    public ModuleAssemblyService()
    {
        Assemblies = new List<Assembly>();
    }

    /// <summary>
    /// Load assemblies from the list of modules
    /// </summary>
    /// <param name="modules"></param>
    /// <returns></returns>
    public async Task<bool> GetAssemblies(List<Module> modules)
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
                Assemblies = Assemblies.Append(assembly);
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