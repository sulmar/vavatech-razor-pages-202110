using Bogus;
using Vavatech.RazorPages.IRepositories;
using Vavatech.RazorPages.Models;

namespace Vavatech.RazorPages.FakeRepositories
{
    public class FakeInvoiceRepository : FakeEntityRepository<Invoice>, IInvoiceRepository
    {
        public FakeInvoiceRepository(Faker<Invoice> faker) : base(faker)
        {
        }
    }


}
