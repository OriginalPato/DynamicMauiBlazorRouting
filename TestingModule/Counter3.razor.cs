using DynamicBlazor.Services;
using DynamicBlazorUi.SourceGenerator.Common;
using Microsoft.AspNetCore.Components;
using TestingModule.Services;

namespace TestingModule;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Counter3 : ComponentBase
{
    [Inject] private SharedCounterService SharedCounterService { get; set; }

    // [InjectModuleService]
    // private ModuleOnlyService _moduleOnlyService;

    [InjectModuleService]
    private ModuleOnlyService2 _moduleOnlyService2;
    [InjectModuleService]
    private ITestService _testService;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await RegisterRemoteServices();
    }

    private void Inc()
    {
        SharedCounterService.IncrementCounter();
    }

    private void Inc2()
    {
        // _moduleOnlyService.Increment();
    }
    private void Inc22()
    {
        // _moduleOnlyService2.Increment();
    }

    private void Inc3()
    {
        // _testService.DoThing();
    }
}