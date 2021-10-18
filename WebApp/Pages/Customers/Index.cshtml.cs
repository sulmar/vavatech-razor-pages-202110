using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace WebApp.Pages.Customers
{
    public class IndexModel : PageModel
    {
        public IEnumerable<Customer> Customers { get; set; }

        private readonly ICustomerRepository customerRepository;

        public IndexModel(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void OnGet()
        {
            Customers = customerRepository.Get();
        }
    }
}
