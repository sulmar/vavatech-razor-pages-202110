using System;
using System.Collections.Generic;
using System.Text;

namespace Vavatech.RazorPages.Models.SearchCriterias
{

    public abstract class SearchCriteria : Base
    {

    }

    public class CustomerSearchCriteria : SearchCriteria
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public Gender? Gender { get; set; }
        public CustomerGroup CustomerGroup { get; set; }

    }
}
