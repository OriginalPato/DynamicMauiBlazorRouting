using DynamicBlazor.Services;
using RazorClassLibraryTesting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibraryTesting
{
    public class RazorClassLibraryRegister
    {
        private readonly IRemoteDependencyInjector _remoteDependencyInjector;

        public RazorClassLibraryRegister(IRemoteDependencyInjector remoteDependencyInjector)
        {
            _remoteDependencyInjector = remoteDependencyInjector;
        }

        public void Register()
        {
            _remoteDependencyInjector.RegisterSingleton<IBaseSharedService, RazorLibraryService>();
        }
    }
}
