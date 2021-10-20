namespace Vavatech.RazorPages.Models
{

    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender? Gender { get; set; }
        public decimal Salary { get; set; }
        public bool IsRemoved { get; set; }
        public Address InvoiceAddress { get; set; }
        public Address ShippedAddress { get; set; }
        public CustomerGroup CustomerGroup { get; set; }
    }

    public class Address : Base
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

    }

    public enum Gender
    {
        Male,
        Female
    }
    
}
