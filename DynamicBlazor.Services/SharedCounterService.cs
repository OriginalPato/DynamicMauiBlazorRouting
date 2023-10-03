namespace DynamicBlazor.Services;

public class SharedCounterService
{
    private int _counter;

    public int GetCounter() => _counter;

    public void IncrementCounter()
    {
        _counter++;
        
    }
}