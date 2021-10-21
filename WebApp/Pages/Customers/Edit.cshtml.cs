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
    public class EditModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }

        private readonly ICustomerRepository customerRepository;
        private readonly ICityRepository cityRepository;
        private readonly ICustomerGroupRepository customerGroupRepository;

        public IEnumerable<string> Cities { get; set; }
        public IEnumerable<SelectListItem> CityItems { get; set; }
        public IEnumerable<SelectListItem> CustomerGroupItems { get; set; }
        public SelectList CustomerGroupList { get; set; }

        public EditModel(
            ICustomerRepository customerRepository, 
            ICityRepository cityRepository, 
            ICustomerGroupRepository customerGroupRepository)
        {
            this.customerRepository = customerRepository;
            this.cityRepository = cityRepository;
            this.customerGroupRepository = customerGroupRepository;
        }

        public void OnGet(int id)
        {
            Customer = customerRepository.Get(id);

            Load();

        }

        private void Load()
        {
            Cities = cityRepository.Get();

            // Mapowanie z u¿yciem wyra¿enia Lambda
            CityItems = Cities
                .OrderBy(city => city)
                .Select(city => new SelectListItem { Value = city, Text = city })
                .ToList();


            // Mapowanie z u¿yciem Linqa
            //CityItems = (from city in Cities
            //             orderby city
            //             select new SelectListItem { Value = city, Text = city }).ToList();

            var customerGroups = customerGroupRepository.Get();

            CustomerGroupItems = customerGroups
                .Select(cg => new SelectListItem { Value = cg.Id.ToString(), Text = cg.Name })
                .ToList();

            CustomerGroupList = new SelectList(customerGroups, nameof(CustomerGroup.Id), nameof(CustomerGroup.Name));
        }

        // Wersja bez BindProperty
        //public void OnPost(Customer customer)
        //{
        //    Customer = customer;

        //    customerRepository.Update(customer);            
        //}

        // Wersja z BindProperty
        //public IActionResult OnPost()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        customerRepository.Update(Customer);

        //        return RedirectToPage("Index");
        //    }

        //    else
        //    {
        //        Load();

        //        return Page();
        //    }
        //}

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Load();

                return Page();
            }


            customerRepository.Update(Customer);

            return RedirectToPage("Index");

      
        }

        /*
        public IActionResult OnPost(Customer customer)
        {
            customerRepository.Update(customer);

            // Typ anonimowy
            var customerInfo = new { Imie = customer.FirstName, Nazwisko = customer.LastName };            


            // Przekierowanie do listy
           // return RedirectToPage("Index");

            // Przekierowanie do szczegó³ów z przekazaniem parametru
            // Zastosowanie typu anonimowego
            return RedirectToPage("Details", new { Id = customer.Id } );
        }
        */
    }

    //public class CustomerInfo
    //{
    //    public string Imie { get; set; }
    //    public string Nazwisko { get; set; }
    //}

   
}
