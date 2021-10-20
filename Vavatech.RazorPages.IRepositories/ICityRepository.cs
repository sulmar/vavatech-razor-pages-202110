using System;
using System.Collections.Generic;
using System.Text;

namespace Vavatech.RazorPages.IRepositories
{
    public interface ICityRepository
    {
        IEnumerable<string> Get();
    }
}
