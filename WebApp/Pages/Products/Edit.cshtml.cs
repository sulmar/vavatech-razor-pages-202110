using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        private readonly IProductRepository productRepository;
        private readonly ITagRepository tagRepository;

        [BindProperty]
        public IFormFile Attachment { get; set; }

        [BindProperty]
        public List<IFormFile> Attachments { get; set; }

        [BindProperty]
        public IEnumerable<int> SelectedTags { get; set; }

        public SelectList TagList { get; set; }

        public EditModel(IProductRepository productRepository, ITagRepository tagRepository)
        {
            this.productRepository = productRepository;
            this.tagRepository = tagRepository;
        }

        public void OnGet()
        {
            Load();
        }

        private void Load()
        {
            Product = productRepository.Get(Id);

            TagList = new SelectList(tagRepository.Get(), nameof(Tag.Id), nameof(Tag.Name));

            SelectedTags = Product.Tags.Select(tag => tag.Id);
        }

        public IActionResult OnPost()
        {
            Product.Tags = SelectedTags.Select(id => tagRepository.Get(id));

            if (Product.Tags.Count() < 3)
            {
                ModelState.AddModelError(nameof(SelectedTags), "Wybierz przynajmniej 3 tagi");
            }

            if (!ModelState.IsValid)
            {
                Load();
                return Page();
            }

            productRepository.Update(Product);

            return RedirectToPage("Index");
        }
        

        public IActionResult AddAttachment()
        {
            return Page();
        }

        public void OnPostAddAttachment()
        {
            Validate(Attachment);

            if (ModelState.IsValid)
            {
                Product.Photo = GetContent(Attachment);
            }
            

            Load();
        }

        private byte[] GetContent(IFormFile attachment)
        {
            MemoryStream memoryStream = new MemoryStream();
            attachment.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }

        private void Validate(IFormFile attachment)
        {
            if (attachment.ContentType != "image/png")
            {
                ModelState.AddModelError("Attachment", "B³êdny format pliku");
            }

            if (attachment.Length > 1_000_000)
            {
                ModelState.AddModelError("Attachment", "Przekroczony rozmiar pliku");
            }
        }

           
        

        public void OnPostAddAttachments()
        {
            foreach (var attachment in Attachments)
            {
                Validate(attachment);
            }
        }
    }
}
