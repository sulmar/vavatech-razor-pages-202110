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
    public class DetailsModel : PageModel
    {
        //public int Id { get; set; }

        //public void OnGet(int id)
        //{
        //    Id = id;
        //}

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Customer Customer { get; set; }

        private readonly ICustomerRepository customerRepository;

        public DetailsModel(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void OnGet()
        {
            Customer = customerRepository.Get(Id);
        }
    }
}
