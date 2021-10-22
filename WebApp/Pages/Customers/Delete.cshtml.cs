using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace WebApp.Pages.Customers
{
    public static class ISessionExtensions
    {
        public static string ToFlatPolish(this string text)
        {
            return text.Replace("³", "l");
        }

        // Metoda rozszerzaj¹ca (extension method)
        // Metoda generyczna (szablon metody)
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var json = JsonSerializer.Serialize(value);

            byte[] data = Encoding.Unicode.GetBytes(json);

            session.Set(key, data);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            byte[] data = session.Get(key);

            var json = Encoding.Unicode.GetString(data);

            T value = JsonSerializer.Deserialize<T>(json);

            return value;
        }

    }

    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public Customer Customer { get; set; }

        private readonly ICustomerRepository customerRepository;
        private readonly ILogger<DeleteModel> logger;

        public DeleteModel(ICustomerRepository customerRepository, ILogger<DeleteModel> logger)
        {
            this.customerRepository = customerRepository;
            this.logger = logger;
        }

        public void OnGet()
        {
            Customer = customerRepository.Get(Id);

            logger.LogInformation("SessionId {0}", HttpContext.Session.Id);


            HttpContext.Session.SetString("imie", Customer.FirstName);
            HttpContext.Session.SetString("nazwisko", Customer.LastName);

            // serializacja
            //var json = JsonSerializer.Serialize(Customer);

            //byte[] data = Encoding.Unicode.GetBytes(json);

            //HttpContext.Session.Set("klient", data);

            HttpContext.Session.SetObject<Customer>("klient", Customer);

        }

        public IActionResult OnPost([FromServices]INotyfService notyfService)
        {
            customerRepository.Remove(Id);

            notyfService.Success($"Klient {Customer.FirstName} {Customer.LastName} zosta³ usuniêty");
            
            string imie = HttpContext.Session.GetString("imie");
            string nazwisko = HttpContext.Session.GetString("nazwisko");

            //byte[] data = HttpContext.Session.Get("klient");
            //var json = Encoding.Unicode.GetString(data);
            //// deserializacja
            //Customer = JsonSerializer.Deserialize<Customer>(json);

            Customer = HttpContext.Session.GetObject<Customer>("klient");

            notyfService.Warning($"Klient {imie} {nazwisko} zosta³ usuniêty");

            HttpContext.Session.Remove("imie");
            HttpContext.Session.Remove("nazwisko");

            return RedirectToPage("Index");
        }
    }
}
