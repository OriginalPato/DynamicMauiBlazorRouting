namespace TestingModule.Services;

public class ModuleOnlyService2
{
    private int _iter;
    public ModuleOnlyService2()
    {
        _iter = 10000;
    }

    public int GetNext()
    {
        return _iter;
    }

    public void Increment()
    {
        _iter++;
    }
}