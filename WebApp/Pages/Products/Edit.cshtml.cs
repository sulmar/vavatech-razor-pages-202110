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
    public class EditModel : PageModel
    {
        public Product Product { get; set; }

        private readonly IProductRepository productRepository;

        public EditModel(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void OnGet(int id)
        {
            Product = productRepository.Get(id);
        }

        public IActionResult OnPost(Product product)
        {
            productRepository.Update(product);

            return RedirectToPage("Index");
        }
    }
}
