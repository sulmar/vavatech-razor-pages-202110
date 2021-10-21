using System;
using System.Collections.Generic;
using Vavatech.RazorPages.Models;
using Vavatech.RazorPages.Models.SearchCriterias;

namespace Vavatech.RazorPages.IRepositories
{
    public interface ICustomerRepository : IEntityRepository<Customer>
    {
        bool IsExists(Customer customer, string email);

        // IEnumerable<Customer> Get(string firstName, string lastName, decimal? salaryFrom, decimal? salaryTo, Gender? gender);

        IEnumerable<Customer> Get(CustomerSearchCriteria searchCriteria);

    }

}
