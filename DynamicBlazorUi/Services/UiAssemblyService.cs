using System.Net;
using System.Reflection;

namespace DynamicBlazorUi.Services;

public interface IUiAssemblyService
{
    IEnumerable<Assembly> Assemblies { get; }
    Task<bool> GetAssemblies();
}

public class UiAssemblyService : IUiAssemblyService
{
    public IEnumerable<Assembly> Assemblies { get; private set; }

    public UiAssemblyService()
    {
        Assemblies = new List<Assembly>();
    }

    public async Task<bool> GetAssemblies()
    {
        using var client = new HttpClient();
        //Obviously this would hit and api and give a link to where we would then download the dll from a CDN (Virtually no cost)
        var res = await client.GetByteArrayAsync(
            "https://blazorhostedassembly.blob.core.windows.net/testing/RazorClassLibrary1.dll");
        try
        {
            var a = Assembly.Load(res);
            Assemblies = Assemblies.Append(a);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
        return true;
    }
}