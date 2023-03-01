namespace TestingModule.Services;

public class TestService : ITestService
{
    private int _counter = 6;
    public void DoThing()
    {
        _counter++;
    }

    public int GetVal() => _counter;
}