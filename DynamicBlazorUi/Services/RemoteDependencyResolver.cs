using System.Diagnostics.CodeAnalysis;

namespace DynamicBlazorUi.Services;

public class RemoteDependencyResolver : IRemoteDependencyResolver
{
    public T Resolve<T>() where T : class, new()
    {
        var service = DependencyService.Resolve<T>();
        if (service != null) { return service; }
        DependencyService.RegisterSingleton(new T());
        service = DependencyService.Resolve<T>();
        return service;
    }

    public TService Resolve<TService, TImplementation>() where TService : class where TImplementation : class, TService
    {
        var service = DependencyService.Resolve<TService>();
        if (service != null) { return service; }
        DependencyService.Register<TService, TImplementation>();
        service = DependencyService.Resolve<TService>();
        return service;
    }
}