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
        protected readonly ICollection<TEntity> entities;

        public FakeEntityRepository(Faker<TEntity> faker)
        {
            entities = faker.Generate(100);
        }

        public virtual void Add(TEntity entity)
        {
            int id = entities.Max(c => c.Id);

            entity.Id = ++id;

            entities.Add(entity);
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return entities;
        }

        public virtual TEntity Get(int id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }

        public virtual void Remove(int id)
        {
            TEntity entity = Get(id);
            entities.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }


}
