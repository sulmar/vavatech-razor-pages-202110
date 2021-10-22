using System;
using System.Collections.Generic;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;
using Vavatech.RazorPages.Models.SearchCriterias;
using Microsoft.Extensions.Caching.Memory;

namespace Vavatech.RazorPages.FakeRepositories
{
    // Wzorzec Proxy 
    public class CacheCustomerRepository : ICustomerRepository
    {
        private readonly IMemoryCache memoryCache;
        private readonly ICustomerRepository customerRepository;

        public CacheCustomerRepository(IMemoryCache memoryCache, ICustomerRepository customerRepository)
        {
            this.memoryCache = memoryCache;
            this.customerRepository = customerRepository;
        }

        public void Add(Customer entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> Get()
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            string key = $"customer-{id}";

            if (memoryCache.TryGetValue(key, out Customer customer))
            {

            }
            else
            {
                customer = customerRepository.Get(id);

                memoryCache.Set(key, customer);
            }

            return customer;
        }

        public bool IsExists(Customer customer, string email)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }


}
