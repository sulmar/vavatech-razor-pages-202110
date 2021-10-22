using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace WebApp.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }

        private readonly ICustomerRepository customerRepository;

        public DeleteModel(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void OnGet()
        {
            Customer = customerRepository.Get(Id);
        }

        public IActionResult OnPost([FromServices]INotyfService notyfService)
        {
            customerRepository.Remove(Id);

            notyfService.Success($"Klient {Customer.FirstName} {Customer.LastName} zosta³ usuniêty");

            return RedirectToPage("Index");
        }
    }
}
