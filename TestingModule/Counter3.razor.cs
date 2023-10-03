using DynamicBlazor.Services;
using Maui.Blazor.CompositeUi;
using Microsoft.AspNetCore.Components;
using TestingModule.Services;

namespace TestingModule;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Counter3 : ComponentBase
{
    [Inject] private IRemoteDependencyResolver RemoteDependencyResolver { get; set; }
    [Inject] private SharedCounterService SharedCounterService { get; set; }
    private ModuleOnlyService _moduleOnlyService;
    private ITestService _testService;
    private IModuleRegisteredService _moduleRegisteredService;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _moduleRegisteredService = RemoteDependencyResolver.Resolve<IModuleRegisteredService, ModuleRegisteredService>();
        _moduleOnlyService = RemoteDependencyResolver.Resolve<ModuleOnlyService>();
        _testService = RemoteDependencyResolver.Resolve<ITestService, TestService>();
    }

    private void Inc()
    {
        SharedCounterService.IncrementCounter();
    }

    private void Inc2()
    {
        _moduleOnlyService.Increment();
    }

    private void Inc3()
    {
        _testService.DoThing();
    }
    
    private void Dec1()
    {
        _moduleRegisteredService.DoThing();
    }
}