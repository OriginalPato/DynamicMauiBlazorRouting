namespace DynamicBlazor.Services;

public class SharedCounterService
{
    private int _counter;

    public SharedCounterService()
    {
        _counter = 0;
    }
    
    public int GetCounter() => _counter;

    public void IncrementCounter()
    {
        _counter++;
        
    }
}