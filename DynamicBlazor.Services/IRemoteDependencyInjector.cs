using System.Diagnostics.CodeAnalysis;

namespace DynamicBlazor.Services
{
    public interface IRemoteDependencyInjector
    {
        void RegisterSingleton<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] TImpl>()
            where T : class
            where TImpl : class, T;
    }
}