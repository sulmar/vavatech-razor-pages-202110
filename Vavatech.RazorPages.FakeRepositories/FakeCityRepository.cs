using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.RazorPages.IRepositories;

namespace Vavatech.RazorPages.FakeRepositories
{
    public class FakeCityRepository : ICityRepository
    {
        private readonly IEnumerable<string> cities;

        public FakeCityRepository()
        {
            cities = new List<string>
            {
                "Warszawa",
                "Poznań",
                "Bydgoszcz"
            };
        }

        public IEnumerable<string> Get()
        {
            return cities;
        }
    }
}
