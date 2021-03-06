using Bogus;
using System;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.FakeRepositories
{
    // Install-Package Bogus
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker(Faker<Address> addressFaker, ICustomerGroupRepository customerGroupRepository)
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.FirstName, f => f.Person.FirstName);
            RuleFor(p => p.LastName, f => f.Person.LastName);
            RuleFor(p => p.Gender, f => (Gender) f.Person.Gender);

            // RuleFor(p => p.Email, f => f.Person.Email);
            // {firstName}.{lastName}@domain.com

            RuleFor(p => p.Email, (f, customer) => $"{customer.FirstName}.{customer.LastName}@domain.com");

            RuleFor(p => p.Salary, f => Math.Round( f.Random.Decimal(1, 1000), 0));
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
            RuleFor(p => p.InvoiceAddress, f => addressFaker.Generate());
            RuleFor(p => p.ShippedAddress, f => addressFaker.Generate());

            RuleFor(p => p.CustomerGroup, f => f.PickRandom(customerGroupRepository.Get()));
        }
    }


}
