using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.IRepositories
{
    public interface ITagRepository
    {
        IEnumerable<Tag> Get();
        Tag Get(int id);
    }
}
