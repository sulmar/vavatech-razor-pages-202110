using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;
using Vavatech.RazorPages.Models.SearchCriterias;

namespace WebApp.Pages.Customers
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public CustomerSearchCriteria SearchCriteria { get; set; }

        public IEnumerable<Customer> Customers { get; set; }
        public SelectList CustomerGroupList { get; set; }

        private readonly ICustomerRepository customerRepository;
        private readonly ICustomerGroupRepository customerGroupRepository;

        [TempData]
        public string imie { get; set; }

        public IndexModel(ICustomerRepository customerRepository, ICustomerGroupRepository customerGroupRepository)
        {
            this.customerRepository = customerRepository;
            this.customerGroupRepository = customerGroupRepository;
        }

        public void OnGet()
        {
            // Customers = customerRepository.Get();
            Load();
        }

        private void Load()
        {
            var customerGroups = customerGroupRepository.Get();

            CustomerGroupList = new SelectList(customerGroups, nameof(CustomerGroup.Id), nameof(CustomerGroup.Name));
        }

        public void OnPost()
        {
            Customers = customerRepository.Get(SearchCriteria);

            Load();
        }
    }
}
