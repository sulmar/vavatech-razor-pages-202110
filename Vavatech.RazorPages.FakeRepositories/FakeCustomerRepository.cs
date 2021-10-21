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
        private readonly ICustomerGroupRepository customerGroupRepository;

        public FakeCustomerRepository(Faker<Customer> faker, ICustomerGroupRepository customerGroupRepository)
            : base(faker)
        {
            this.customerGroupRepository = customerGroupRepository;
        }

        public bool IsExists(Customer customer, string email)
        {
            return entities
                .Where(c => c.Id != customer.Id)
                .Select(c => c.Email)
                .Contains(email);
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
            customer.InvoiceAddress = entity.InvoiceAddress;
            customer.ShippedAddress = entity.ShippedAddress;
            customer.CustomerGroup = customerGroupRepository.Get(entity.CustomerGroup.Id);
            customer.Email = entity.Email;

        }
    }


}
