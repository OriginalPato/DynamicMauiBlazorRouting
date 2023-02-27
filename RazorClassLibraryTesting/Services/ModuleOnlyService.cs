using DynamicBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibraryTesting.Services
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
