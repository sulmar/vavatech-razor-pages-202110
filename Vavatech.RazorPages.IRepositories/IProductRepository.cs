using System;
using System.Collections.Generic;
using Vavatech.RazorPages.Models;
using Vavatech.RazorPages.Models.SearchCriterias;

namespace Vavatech.RazorPages.IRepositories
{

    public interface IProductRepository : IEntityRepository<Product>
    {
        IEnumerable<Product> GetByColor(string color);
        IEnumerable<Product> GetByPrice(decimal fromUnitPrice, decimal toUnitPrice);
        IEnumerable<Product> Get(ProductSearchCriteria searchCriteria);
    }

}
