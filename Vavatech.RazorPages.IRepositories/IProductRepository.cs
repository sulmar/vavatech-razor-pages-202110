using System;
using System.Collections.Generic;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.IRepositories
{

    public interface IProductRepository : IEntityRepository<Product>
    {
        IEnumerable<Product> GetByColor(string color);
        IEnumerable<Product> GetByPrice(decimal fromUnitPrice, decimal toUnitPrice);
    }

}
