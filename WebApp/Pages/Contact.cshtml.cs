using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class ContactModel : PageModel
    {        
        public string Message { get; set; }

        public void OnGet(string message, string countryCode)
        {
            Message = $"{message}! {countryCode} {DateTime.UtcNow}";
        }
    }
}
