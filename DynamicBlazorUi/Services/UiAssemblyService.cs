﻿using System.Reflection;
using DynamicBlazor.Api.Controllers;

namespace DynamicBlazorUi.Services;

public interface IUiAssemblyService
{
    IEnumerable<Assembly> Assemblies { get; }
    Task<bool> GetAssemblies(List<Feature> features);
}

public class UiAssemblyService : IUiAssemblyService
{
    public IEnumerable<Assembly> Assemblies { get; private set; }

    public UiAssemblyService()
    {
        Assemblies = new List<Assembly>();
    }

    public async Task<bool> GetAssemblies(List<Feature> features)
    {
        using var client = new HttpClient();
        var errors = 0;
        foreach (var feature in features)
        {
            var res = await client.GetByteArrayAsync(
                feature.DownloadUrl);
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