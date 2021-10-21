using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;
using Vavatech.RazorPages.Models.SearchCriterias;

namespace Vavatech.RazorPages.InMemoryRepositories
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

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

        public Customer Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public bool IsExists(Customer customer, string email)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
