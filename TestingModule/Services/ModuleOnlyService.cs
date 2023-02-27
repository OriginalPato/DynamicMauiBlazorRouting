namespace TestingModule.Services
{
    public class ModuleOnlyService
    {
        private int _iter;
        public ModuleOnlyService()
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
}
