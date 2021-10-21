using System.ComponentModel.DataAnnotations;

namespace Vavatech.RazorPages.Models
{
    public class Customer : BaseEntity
    {        
        [Required, StringLength(20, MinimumLength = 3)]
        public string FirstName { get; set; }

        [StringLength(30, MinimumLength = 3, ErrorMessage = "Nazwisko powinno zawierać od 3 do 30 znaków")]
        [Required] 
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        [Range(1, 1000)]
        public decimal Salary { get; set; }
        public bool IsRemoved { get; set; }
        public Address InvoiceAddress { get; set; }
        public Address ShippedAddress { get; set; }
        public CustomerGroup CustomerGroup { get; set; }

        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }

    public class Address : Base
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

    }

    // Install-Package System.ComponentModel.DataAnnotations
    public enum Gender
    {
        [Display(Name = "Mężczyzna")]
        Male,

        [Display(Name = "Kobieta")]
        Female
    }
    
}
