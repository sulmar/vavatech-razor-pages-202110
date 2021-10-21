using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;
using Vavatech.RazorPages.Models.SearchCriterias;

namespace Vavatech.RazorPages.FakeRepositories
{
    public class FakeProductRepository : FakeEntityRepository<Product>, IProductRepository
    {
        public FakeProductRepository(Faker<Product> faker) : base(faker)
        {
        }

        public IEnumerable<Product> Get(ProductSearchCriteria searchCriteria)
        {
            IQueryable<Product> products = entities.AsQueryable();

            if (!string.IsNullOrEmpty(searchCriteria.Color))
            {
                products = products.Where(p => p.Color == searchCriteria.Color);
            }

            if (searchCriteria.UnitPriceFrom.HasValue)
            {
                products = products.Where(p => p.UnitPrice >= searchCriteria.UnitPriceFrom);
            }

            if (searchCriteria.UnitPriceTo.HasValue)
            {
                products = products.Where(p => p.UnitPrice <= searchCriteria.UnitPriceTo);
            }


            return products;
        }

        public IEnumerable<Product> GetByColor(string color)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetByPrice(decimal fromUnitPrice, decimal toUnitPrice)
        {
            throw new NotImplementedException();
        }

        public override void Update(Product entity)
        {
            Product product = Get(entity.Id);

            product.Name = entity.Name;
            product.Size = entity.Size;
            product.Color = entity.Color;
            product.UnitPrice = entity.UnitPrice;
            product.Weight = entity.Weight;
            product.BarCode = entity.BarCode;
            product.Tags = entity.Tags;
            product.Photo = entity.Photo;
        }
    }


}
