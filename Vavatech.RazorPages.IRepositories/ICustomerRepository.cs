using System;
using System.Collections.Generic;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.IRepositories
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        bool IsExists(Customer customer, string email);
    }

}
