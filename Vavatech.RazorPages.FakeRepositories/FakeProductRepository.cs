using Bogus;
using System;
using System.Collections.Generic;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.FakeRepositories
{
    public class FakeProductRepository : FakeEntityRepository<Product>, IProductRepository
    {
        public FakeProductRepository(Faker<Product> faker) : base(faker)
        {
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
        }
    }


}
