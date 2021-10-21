using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
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
            return Url;
        }

        public override bool IsValid(object value)
        {
            string website = value.ToString();
            HttpClient client = new HttpClient();
            var response = client.GetAsync(website).Result;

            return response.IsSuccessStatusCode;
        }
    }
}
