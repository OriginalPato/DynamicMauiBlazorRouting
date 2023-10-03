using System.Diagnostics.CodeAnalysis;

namespace Maui.Blazor.CompositeUi;

// All the code in this file is included in all platforms.
public interface IRemoteDependencyResolver
{
    T Resolve<T>() where T : class, new();
    TService Resolve<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>() 
        where TService : class 
        where TImplementation : class, TService;
}