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
        RazorLibraryService razorLibraryService;
        [Inject] IRemoteDependencyResolver remoteDependencyResolver { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            razorLibraryService = remoteDependencyResolver.Resolve<RazorLibraryService>(new RazorLibraryService());
        }

        void inc()
        {
            TestService.IncrementCounter();
        }
        void inc2()
        {
            razorLibraryService.Increment();
        }
    }
}
