using Maui.Blazor.CompositeUi;
using Microsoft.Extensions.DependencyInjection;
using TestingModule.Services;

namespace TestingModule;

public class ModuleInstaller : IModuleInstaller
{
    private readonly IRemoteDependencyResolver _remoteDependencyResolver;

    public ModuleInstaller(IRemoteDependencyResolver remoteDependencyResolver)
    {
        _remoteDependencyResolver = remoteDependencyResolver;
    }
    public void Install()
    {
        _remoteDependencyResolver.Resolve<IModuleRegisteredService, ModuleRegisteredService>();
    }
}