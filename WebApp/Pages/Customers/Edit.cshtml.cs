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

        public IEnumerable<string> Cities { get; set; }
        public IEnumerable<SelectListItem> CityItems { get; set; }

        public EditModel(ICustomerRepository customerRepository, ICityRepository cityRepository)
        {
            this.customerRepository = customerRepository;
            this.cityRepository = cityRepository;
        }

        public void OnGet(int id)
        {
            Customer = customerRepository.Get(id);

            Load();

        }

        private void Load()
        {
            Cities = cityRepository.Get();

            CityItems = Cities
                .OrderBy(city => city)
                .Select(city => new SelectListItem { Value = city, Text = city })
                .ToList();

            CityItems = (from city in Cities
                         orderby city
                         select new SelectListItem { Value = city, Text = city }).ToList();
        }

        // Wersja bez BindProperty
        //public void OnPost(Customer customer)
        //{
        //    Customer = customer;

        //    customerRepository.Update(customer);            
        //}

        // Wersja z BindProperty
        public void OnPost()
        {
            customerRepository.Update(Customer);

            Load();
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
