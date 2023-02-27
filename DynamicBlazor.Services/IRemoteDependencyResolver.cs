namespace DynamicBlazor.Services
{
    public interface IRemoteDependencyResolver
    {
        T Resolve<T>() where T : class, new();
    }
}