using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Pages
{

    public class TestPageRemote : PageRemoteAttribute
    {
        
    }

    public class RestApiRemote : RemoteAttributeBase
    {
        public string Url { get; set; }

        protected override string GetUrl(ClientModelValidationContext context)
        {
            var services = context.ActionContext.HttpContext.RequestServices;
            var factory = services.GetRequiredService<Microsoft.AspNetCore.Mvc.Routing.IUrlHelperFactory>();
            var urlHelper = factory.GetUrlHelper(context.ActionContext);
            var page = context.ActionContext.RouteData.Values["page"] as string;

            Url = urlHelper.Page(page);
            if (Url == null)
            {
                throw new InvalidOperationException();
            }

            return Url;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string website = value.ToString();
            HttpClient client = new HttpClient();

            var response = client.GetAsync(website).Result;

            if (response.IsSuccessStatusCode)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(base.ErrorMessage);
            }
        }

        //public override bool IsValid(object value)
        //{
        //    string website = value.ToString();
        //    HttpClient client = new HttpClient();
            
        //    var response = client.GetAsync(website).Result;

        //    return response.IsSuccessStatusCode;
        //}
    }
}
