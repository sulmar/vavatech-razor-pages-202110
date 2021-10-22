using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;
using Vavatech.RazorPages.Models.SearchCriterias;

namespace Vavatech.RazorPages.DbEfRepositories
{
    public class DbCustomerRepository : ICustomerRepository
    {
        private readonly ShopContext context;

        public DbCustomerRepository(ShopContext context)
        {
            this.context = context;
        }

        private DbSet<Customer> entities => context.Customers;

        public void Add(Customer entity)
        {
            context.Entry(entity.CustomerGroup).State = EntityState.Unchanged;

            entities.Add(entity);
            context.SaveChanges();
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

            if (searchCriteria.CustomerGroup != null)
            {
                customers = customers.Where(c => c.CustomerGroup.Id == searchCriteria.CustomerGroup.Id);
            }

            return customers.ToList();
        }

        public IEnumerable<Customer> Get()
        {
            return entities.ToList();
        }

        public Customer Get(int id)
        {
            return context.Customers.Find(id);
        }

        public bool IsExists(Customer customer, string email)
        {
            return entities
               .Where(c => c.Id != customer.Id)
               .Select(c => c.Email)
               .Contains(email);
        }

        public void Remove(int id)
        {
            entities.Remove(Get(id));
            context.SaveChanges();
        }

        public void Update(Customer entity)
        {
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
