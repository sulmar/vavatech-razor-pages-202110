using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.FakeRepositories
{
    public class FakeCustomerGroupRepository : ICustomerGroupRepository
    {
        private readonly IEnumerable<CustomerGroup> customerGroups;

        public FakeCustomerGroupRepository()
        {
            customerGroups = new List<CustomerGroup>
            {
                new CustomerGroup { Id = 1, Name = "Silver", Discount = 0.1m },
                new CustomerGroup { Id = 2, Name = "Gold", Discount = 0.25m },
                new CustomerGroup { Id = 3, Name = "Platine", Discount = 0.5m },
            };
        }

        public IEnumerable<CustomerGroup> Get() => customerGroups;
        
    }
}
