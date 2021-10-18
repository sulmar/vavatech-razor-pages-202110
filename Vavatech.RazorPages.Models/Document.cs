namespace Vavatech.RazorPages.Models
{
    public abstract class Document : BaseEntity
    {
        public decimal TotalAmount { get; set; }

        public Customer Customer { get; set; }
    }
    
}
