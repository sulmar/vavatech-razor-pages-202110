using Bogus;
using System;
using System.Collections.Generic;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;
using System.Linq;

namespace Vavatech.RazorPages.FakeRepositories
{

    public class FakeCustomerRepository : FakeEntityRepository<Customer>, ICustomerRepository
    {
        public FakeCustomerRepository(Faker<Customer> faker)
            : base(faker)
        {
            
        }
    }


}
