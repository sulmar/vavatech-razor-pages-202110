using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace WebApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository productRepository;

        public IEnumerable<Product> Products { get; set; }


        public IndexModel(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        

        public void OnGet()
        {
            Products = productRepository.Get();
        }
    }
}
