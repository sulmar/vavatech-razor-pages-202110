using Bogus;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.FakeRepositories
{
    public class AddressFaker : Faker<Address>
    {
        public AddressFaker()
        {
            StrictMode(true);
            RuleFor(p => p.City, f => f.Address.City());
            RuleFor(p => p.Street, f => f.Address.StreetAddress());
            RuleFor(p => p.Country, f => f.Address.Country());
            RuleFor(p => p.ZipCode, f => f.Address.ZipCode());
        }
    }


}
