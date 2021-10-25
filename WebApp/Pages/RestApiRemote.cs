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


    public class RestApiRemote : RemoteAttributeBase
    {
        public string Url { get; set; }

        protected override string GetUrl(ClientModelValidationContext context)
        {
            return Url;
        }
    }
}
