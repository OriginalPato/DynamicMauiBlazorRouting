using DynamicBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicBlazorUi.Services
{
    public class RemoteDependencyResolver : IRemoteDependencyResolver
    {
        public T Resolve<T>(T type) where T : class
        {
            var y = typeof(T);
            T res = null;
            res = DependencyService.Resolve<T>();
            if (res != null) { return res; }
            DependencyService.RegisterSingleton<T>(type);
            res = DependencyService.Resolve<T>();
            return res;
        }
    }
}
