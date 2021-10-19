using System.Collections.Generic;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.IRepositories
{
    // Interfejs generyczny (szablon interfejsu)
    public interface IEntityRepository<TEntity>
       // where TEntity : BaseEntity          // constraint (reguła)
    {
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(int id);
    }

}
