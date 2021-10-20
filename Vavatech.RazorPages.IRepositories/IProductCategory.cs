using System.Collections.Generic;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.IRepositories
{
    public interface IProductCategory
    {
        IEnumerable<ProductCategory> Get();

        ProductCategory Get(int id);
    }

}
