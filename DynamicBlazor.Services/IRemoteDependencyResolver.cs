namespace DynamicBlazor.Services
{
    public interface IRemoteDependencyResolver
    {
        T Resolve<T>(T type) where T : class;
    }
}