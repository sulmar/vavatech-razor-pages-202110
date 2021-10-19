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

        public override void Update(Customer entity)
        {
            Customer customer = Get(entity.Id);

            customer.FirstName = entity.FirstName;
            customer.LastName = entity.LastName;
            customer.Salary = entity.Salary;
            customer.IsRemoved = entity.IsRemoved;
            customer.Gender = entity.Gender;
            customer.CreatedDate = entity.CreatedDate;
            customer.ModifiedDate = DateTime.UtcNow;            
        }
    }


}
