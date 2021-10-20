using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace WebApp.Pages.Products
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; }

        private readonly IProductRepository productRepository;
        private readonly ITagRepository tagRepository;

        [BindProperty]
        public IEnumerable<int> SelectedTags { get; set; }

        public SelectList TagList { get; set; }

        public EditModel(IProductRepository productRepository, ITagRepository tagRepository)
        {
            this.productRepository = productRepository;
            this.tagRepository = tagRepository;
        }

        public void OnGet(int id)
        {
            Product = productRepository.Get(id);

            TagList = new SelectList(tagRepository.Get(), nameof(Tag.Id), nameof(Tag.Name));

            SelectedTags = Product.Tags.Select(tag => tag.Id);
        }

        public IActionResult OnPost()
        {
            Product.Tags = SelectedTags.Select(id => tagRepository.Get(id));

            productRepository.Update(Product);

            return RedirectToPage("Index");
        }
    }
}
