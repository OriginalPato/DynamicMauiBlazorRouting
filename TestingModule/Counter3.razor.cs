using DynamicBlazor.Services;
using Microsoft.AspNetCore.Components;
using TestingModule.Services;

namespace TestingModule;

// ReSharper disable once ClassNeverInstantiated.Global
public partial class Counter3 : ComponentBase
{
    [Inject] private IRemoteDependencyResolver RemoteDependencyResolver { get; set; }
    [Inject] private SharedCounterService SharedCounterService { get; set; }
    private ModuleOnlyService _moduleOnlyService;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _moduleOnlyService = RemoteDependencyResolver.Resolve<ModuleOnlyService>();
    }

    private void Inc()
    {
        SharedCounterService.IncrementCounter();
    }

    private void Inc2()
    {
        _moduleOnlyService.Increment();
    }
}