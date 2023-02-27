using DynamicBlazor.Services;
using Microsoft.AspNetCore.Components;
using RazorClassLibraryTesting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibraryTesting
{
    public partial class Counter3 : ComponentBase
    {
        [Inject] IRemoteDependencyResolver remoteDependencyResolver { get; set; }
        [Inject] private SharedCounterService SharedCounterService { get; set; }
        ModuleOnlyService _moduleOnlyService;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _moduleOnlyService = remoteDependencyResolver.Resolve<ModuleOnlyService>(new ModuleOnlyService());
        }

        void inc()
        {
            SharedCounterService.IncrementCounter();
        }
        void inc2()
        {
            _moduleOnlyService.Increment();
        }
    }
}
