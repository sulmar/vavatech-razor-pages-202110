using System;
using System.Collections.Generic;
using System.Text;

namespace Vavatech.RazorPages.IRepositories
{
    public interface IMessageService
    {
        void Send(string message);
    }
}
