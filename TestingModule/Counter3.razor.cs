using DynamicBlazor.Services;
using DynamicBlazorUi.SourceGenerator.Common;
using Microsoft.AspNetCore.Components;
using TestingModule.Services;

namespace TestingModule;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Counter3 : ComponentBase
{
    // [Inject] private IRemoteDependencyResolver RemoteDependencyResolver { get; set; }
    [Inject] private SharedCounterService SharedCounterService { get; set; }
    [InjectModuleService]
    private ModuleOnlyService _moduleOnlyService;
    // [InjectModuleService]
    // private ITestService _testService;

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
        // _testService.DoThing();
    }
}