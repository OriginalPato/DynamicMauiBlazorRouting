﻿using DynamicBlazor.Services;
using Microsoft.AspNetCore.Components;
using TestingModule.Services;

namespace TestingModule
{
    public partial class Counter3 : ComponentBase
    {
        [Inject] IRemoteDependencyResolver remoteDependencyResolver { get; set; }
        [Inject] private SharedCounterService SharedCounterService { get; set; }
        ModuleOnlyService _moduleOnlyService;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _moduleOnlyService = remoteDependencyResolver.Resolve<ModuleOnlyService>();
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
