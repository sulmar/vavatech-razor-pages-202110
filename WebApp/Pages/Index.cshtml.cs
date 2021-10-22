using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly INotyfService notyfService;

        public IndexModel(ILogger<IndexModel> logger, INotyfService notyfService)
        {
            _logger = logger;
            this.notyfService = notyfService;
        }

       
        public bool IsReturned { get; set; }

        public void OnGet()
        {
            var value = Request.Cookies["LastVisit"];

            if (value != null)
            {
                DateTime lastVisit = DateTime.Parse(value);

                notyfService.Information($"Witaj ponownie! Ostatni raz byłeś {lastVisit}");
            }

            
            Response.Cookies.Append("LastVisit", DateTime.UtcNow.ToString());


            var cookiesOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1)
            };


            Response.Cookies.Append("Token", Guid.NewGuid().ToString(), cookiesOptions);

        }

        public void OnPost()
        {
            Response.Cookies.Append("LastVisit", DateTime.UtcNow.ToString());
        }
    }
}
