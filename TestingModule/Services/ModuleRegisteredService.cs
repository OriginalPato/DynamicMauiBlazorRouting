using Maui.Blazor.CompositeUi;
using Microsoft.Extensions.DependencyInjection;

namespace TestingModule.Services;

public interface IModuleRegisteredService
{
    void DoThing();
    int GetVal();
}

public class ModuleRegisteredService : IModuleRegisteredService
{
    private int _counter = 600;
    public void DoThing()
    {
        _counter--;
    }

    public int GetVal() => _counter;
}