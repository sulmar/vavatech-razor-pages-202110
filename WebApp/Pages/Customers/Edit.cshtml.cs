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
    public class EditModel : PageModel
    {
        public Customer Customer { get; set; }

        private readonly ICustomerRepository customerRepository;

        public EditModel(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void OnGet(int id)
        {
            Customer = customerRepository.Get(id);
        }

        public void OnPost(Customer customer)
        {

        }
    }
}
