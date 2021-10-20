using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.FakeRepositories
{
    public class FakeTagRepository : ITagRepository
    {
        private readonly IEnumerable<Tag> tags;

        public FakeTagRepository()
        {
            tags = new List<Tag>
            {
                new Tag { Id = 1, Name = "IT"},
                new Tag { Id = 2, Name = "Book"},
                new Tag { Id = 3, Name = "Family"},
                new Tag { Id = 4, Name = "Work"},
            };
        }

        public IEnumerable<Tag> Get()
        {
            return tags;
        }

        public Tag Get(int id)
        {
            return tags.SingleOrDefault(t => t.Id == id);
        }
    }
}
