using Bogus;
using System;
using System.Collections.Generic;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;
using System.Linq;

namespace Vavatech.RazorPages.FakeRepositories
{
    // Klasa generyczna (Szablon klasy)
    public class FakeEntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly ICollection<TEntity> entities;

        public FakeEntityRepository(Faker<TEntity> faker)
        {
            entities = faker.Generate(100);
        }

        public void Add(TEntity entity)
        {
            int id = entities.Max(c => c.Id);

            entity.Id = ++id;

            entities.Add(entity);
        }

        public IEnumerable<TEntity> Get()
        {
            return entities;
        }

        public TEntity Get(int id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }

        public void Remove(int id)
        {
            TEntity entity = Get(id);
            entities.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }


}
