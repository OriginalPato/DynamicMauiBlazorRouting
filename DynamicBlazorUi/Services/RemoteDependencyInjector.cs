using DynamicBlazor.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicBlazorUi.Services
{
    public class RemoteDependencyInjector : IRemoteDependencyInjector
    {
        public void RegisterSingleton<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] TImpl>()
            where T : class
            where TImpl : class, T
        {
            //DependencyService.Register<TImpl>();
            //var r = DependencyService.Resolve<TImpl>();
            //var u = DependencyService.Get<TImpl>();
        }
    }
}
