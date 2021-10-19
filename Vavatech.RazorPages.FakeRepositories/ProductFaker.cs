using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.RazorPages.Models;


namespace Vavatech.RazorPages.FakeRepositories
{
    public class ProductFaker : Faker<Product>
    {
        public ProductFaker()
        {
            string[] sizes = new string[] { "S", "M", "L", "XL" };

            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Commerce.ProductName());
            RuleFor(p => p.Size, f => f.PickRandom(sizes));
            RuleFor(p => p.BarCode, f => f.Commerce.Ean13());
            RuleFor(p => p.Weight, f => f.Random.Float(100, 1000));
            RuleFor(p => p.UnitPrice, f => Math.Round( f.Random.Decimal(100, 1000), 2));
            
        }
    }
}
