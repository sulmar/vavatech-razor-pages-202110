using Bogus;
using System;
using System.Collections.Generic;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;
using System.Linq;
using Vavatech.RazorPages.Models.SearchCriterias;

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

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            IQueryable<Customer> customers = entities.AsQueryable();

            if (!string.IsNullOrEmpty(searchCriteria.FirstName))
            {
                customers = customers.Where(c => c.FirstName == searchCriteria.FirstName);
            }

            if (!string.IsNullOrEmpty(searchCriteria.LastName))
            {
                customers = customers.Where(c => c.LastName == searchCriteria.LastName);
            }

            if (searchCriteria.SalaryFrom.HasValue)
            {
                customers = customers.Where(c => c.Salary >= searchCriteria.SalaryFrom);
            }

            if (searchCriteria.SalaryTo.HasValue)
            {
                customers = customers.Where(c => c.Salary <= searchCriteria.SalaryTo);
            }

            if (searchCriteria.Gender.HasValue)
            {
                customers = customers.Where(c => c.Gender == searchCriteria.Gender);
            }

            if (searchCriteria.CustomerGroup!=null)
            {
                customers = customers.Where(c => c.CustomerGroup.Id == searchCriteria.CustomerGroup.Id);
            }

            return customers.ToList();

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
