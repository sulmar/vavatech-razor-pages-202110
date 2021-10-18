using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.InMemoryRepositories
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        public decimal Calculate()
        {
            return 0;
        }

        public IEnumerable<Customer> Get()
        {
            var customers = new List<Customer>
            {
                new Customer { FirstName = "John" , LastName = "Smith"},
                new Customer { FirstName = "Ann" , LastName = "Smith"},
                new Customer { FirstName = "Bob" , LastName = "Smith"},
                new Customer { FirstName = "Kate" , LastName = "Smith"},
                new Customer { FirstName = "Adam" , LastName = "Smith"},
            };

            return customers;
        }
    }
}
