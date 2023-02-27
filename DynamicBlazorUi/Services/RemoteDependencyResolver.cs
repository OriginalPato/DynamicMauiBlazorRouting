namespace DynamicBlazorUi.Services;

public class RemoteDependencyResolver : IRemoteDependencyResolver
{
    public T Resolve<T>() where T : class, new()
    {
        var res = DependencyService.Resolve<T>();
        if (res != null) { return res; }
        DependencyService.RegisterSingleton(new T());
        res = DependencyService.Resolve<T>();
        return res;
    }
}