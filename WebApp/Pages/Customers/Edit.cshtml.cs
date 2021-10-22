using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace WebApp.Pages.Customers
{
   // [IgnoreAntiforgeryToken]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }

        [PageRemote(
            ErrorMessage = "Podany adres email ju¿ istnieje",            
            HttpMethod = "post",
            PageHandler = "CheckEmail",
            AdditionalFields = "__RequestVerificationToken, Customer.Id"
            )]

       
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
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        public IEnumerable<string> Cities { get; set; }
        public IEnumerable<SelectListItem> CityItems { get; set; }
        public IEnumerable<SelectListItem> CustomerGroupItems { get; set; }
        public SelectList CustomerGroupList { get; set; }

        public EditModel(
            ICustomerRepository customerRepository, 
            ICityRepository cityRepository, 
            ICustomerGroupRepository customerGroupRepository,
            INotyfService notyfService,
            IMemoryCache memoryCache,
            IDistributedCache distributedCache
            )
        {
            this.customerRepository = customerRepository;
            this.cityRepository = cityRepository;
            this.customerGroupRepository = customerGroupRepository;
            this.notyfService = notyfService;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }

        public void OnGet()
        {
            Load();
        }

        private void Load()
        {
            string key = $"customer-{Id}";

            if (memoryCache.TryGetValue(key, out Customer customer))
            {
                Customer = customer;
            }
            else
            {
                Customer = customerRepository.Get(Id);

                memoryCache.Set(key, Customer);
            }

            //string imie = distributedCache.GetString(key);

            //if (imie==null)
            //{
            //    Customer = customerRepository.Get(Id);

            //    distributedCache.SetString(key, Customer.FirstName);
            //}


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

            Customer.Email = Email;
            customerRepository.Update(Customer);

            string key = $"customer-{Id}";
            memoryCache.Remove(key);

            return RedirectToPage("Index");      
        }

        // {OnPost}{Handler}
        public void OnPostSend([FromServices] IMessageService messageService)
        {
            messageService.Send("Hello World!");

            notyfService.Custom("Komunikat zosta³ wys³any.", 5, "whitesmoke", "fa fa-gear");

            Load();
        }
        

        public void OnGetSend([FromServices] IMessageService messageService)
        {
            messageService.Send("Hello World!");
            Load();
        }

        // Zdalna walidacja (Remote Validation)
        public JsonResult OnPostCheckEmail()
        {
            var isValid = !customerRepository.IsExists(Customer, Email);

            return new JsonResult(isValid);

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
