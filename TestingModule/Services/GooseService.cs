using Maui.Blazor.CompositeUi;
using Microsoft.Extensions.DependencyInjection;

namespace TestingModule.Services;

public interface IGooseService
{
    void DoThing();
    int GetVal();
}

public class GooseService : IGooseService
{
    private int _counter = 600;
    public void DoThing()
    {
        _counter--;
    }

    public int GetVal() => _counter;
}