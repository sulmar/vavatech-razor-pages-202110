using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.IRepositories
{
    public interface ICustomerGroupRepository
    {
        IEnumerable<CustomerGroup> Get();
    }
}
