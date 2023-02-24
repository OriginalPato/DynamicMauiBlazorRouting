namespace DynamicBlazor.Services;

public class TestService
{
    private int _counter;

    public TestService()
    {
        _counter = 0;
    }
    
    public int GetCounter() => _counter;

    public void IncrementCounter()
    {
        _counter++;
        
    }
}