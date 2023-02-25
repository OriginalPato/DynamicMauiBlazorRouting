using DynamicBlazor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibraryTesting.Services
{
    public class RazorLibraryService : IBaseSharedService
    {
        private int _iter;
        public RazorLibraryService()
        {
            _iter = 0;
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
