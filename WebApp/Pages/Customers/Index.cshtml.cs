using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vavatech.RazorPages.Models;

namespace WebApp.Pages.Customers
{
    public class InMemoryCustomerRepository
    {
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

    public class IndexModel : PageModel
    {
        public IEnumerable<Customer> Customers { get; set; }

        public void OnGet()
        {
            InMemoryCustomerRepository customerRepository = new InMemoryCustomerRepository();

            Customers = customerRepository.Get();
        }
    }
}
