using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace WebApp.Pages.Customers
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [RestApiRemote(
          ErrorMessage = "Podana strona nie istnieje",
          HttpMethod = "post",
          AdditionalFields = "__RequestVerificationToken"
            )]
        [BindProperty]
        public string Website { get; set; }

        private readonly ICustomerRepository customerRepository;
        private readonly ICityRepository cityRepository;
        private readonly ICustomerGroupRepository customerGroupRepository;

        public IEnumerable<string> Cities { get; set; }
        public IEnumerable<SelectListItem> CityItems { get; set; }
        public IEnumerable<SelectListItem> CustomerGroupItems { get; set; }
        public SelectList CustomerGroupList { get; set; }

        public CreateModel(
           ICustomerRepository customerRepository,
           ICityRepository cityRepository,
           ICustomerGroupRepository customerGroupRepository)
        {
            this.customerRepository = customerRepository;
            this.cityRepository = cityRepository;
            this.customerGroupRepository = customerGroupRepository;
        }

        public void OnGet()
        {
            Load();

            Customer = Customer.Create();
        }

        private void Load()
        {
            Cities = cityRepository.Get();

            // Mapowanie z u¿yciem wyra¿enia Lambda
            CityItems = Cities
                .OrderBy(city => city)
                .Select(city => new SelectListItem { Value = city, Text = city })
                .ToList();

            var customerGroups = customerGroupRepository.Get();

            CustomerGroupItems = customerGroups
                .Select(cg => new SelectListItem { Value = cg.Id.ToString(), Text = cg.Name })
                .ToList();

            CustomerGroupList = new SelectList(customerGroups, nameof(CustomerGroup.Id), nameof(CustomerGroup.Name));
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Load();

                return Page();
            }

            Customer.Email = Email;
            customerRepository.Add(Customer);

            return RedirectToPage("Index");
        }
    }
}
