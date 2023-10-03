using Microsoft.Maui.Controls;

namespace Maui.Blazor.CompositeUi;

public class RemoteDependencyResolver : IRemoteDependencyResolver
{
    /// <summary>
    /// Register Service. e.g TestService
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T Resolve<T>() where T : class, new()
    {
        var service = DependencyService.Resolve<T>();
        if (service != null) { return service; }
        DependencyService.RegisterSingleton(new T());
        service = DependencyService.Resolve<T>();
        return service;
    }

    /// <summary>
    /// Register Service and Implementation
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    /// <returns></returns>
    public TService Resolve<TService, TImplementation>() where TService : class where TImplementation : class, TService
    {
        var service = DependencyService.Resolve<TService>();
        if (service != null) { return service; }
        DependencyService.Register<TService, TImplementation>();
        service = DependencyService.Resolve<TService>();
        return service;
    }
}