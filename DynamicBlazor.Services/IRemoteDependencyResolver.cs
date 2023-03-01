using System.Diagnostics.CodeAnalysis;

namespace DynamicBlazor.Services;

public interface IRemoteDependencyResolver
{
    T Resolve<T>() where T : class, new();
    TService Resolve<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>() 
        where TService : class 
        where TImplementation : class, TService;
}