using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.DbEfRepositories
{
    public class DbCustomerGroupsRepository : ICustomerGroupRepository
    {
        private readonly ShopContext context;

        public DbCustomerGroupsRepository(ShopContext context)
        {
            this.context = context;
        }

        public IEnumerable<CustomerGroup> Get()
        {
            return context.CustomerGroups.ToList();
        }

        public CustomerGroup Get(int id)
        {
            return context.CustomerGroups.Find(id);
        }
    }
}
