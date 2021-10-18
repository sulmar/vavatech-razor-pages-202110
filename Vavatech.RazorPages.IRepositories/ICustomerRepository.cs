using System;
using System.Collections.Generic;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.IRepositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();
        //Customer Get(int id);
        //void Add(Customer customer);
        //void Update(Customer customer);
        //void Remove(int id);
    }

}
