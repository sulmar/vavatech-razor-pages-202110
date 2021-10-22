using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;
using WebApp.Hubs;

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
        private readonly INotyfService notyfService;
        private readonly IHubContext<CustomersHub> hubContext;

        public IEnumerable<string> Cities { get; set; }
        public IEnumerable<SelectListItem> CityItems { get; set; }
        public IEnumerable<SelectListItem> CustomerGroupItems { get; set; }
        public SelectList CustomerGroupList { get; set; }

        public CreateModel(
           ICustomerRepository customerRepository,
           ICityRepository cityRepository,
           ICustomerGroupRepository customerGroupRepository,
           INotyfService notyfService,
           IHubContext<CustomersHub> hubContext
           )
        {
            this.customerRepository = customerRepository;
            this.cityRepository = cityRepository;
            this.customerGroupRepository = customerGroupRepository;
            this.notyfService = notyfService;
            this.hubContext = hubContext;
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

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Load();

                notyfService.Error($"Popraw b³êdy na stronie.");

                return Page();
            }

            Customer.Email = Email;
            customerRepository.Add(Customer);

            await hubContext.Clients.All.SendAsync("AddedCustomer", Customer);

            notyfService.Success($"Klient {Customer.FirstName} {Customer.LastName} zosta³ dodany");

            return RedirectToPage("Index");
        }
    }
}
