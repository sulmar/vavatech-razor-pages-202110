namespace Vavatech.RazorPages.Models.SearchCriterias
{
    public class ProductSearchCriteria : SearchCriteria
    {
        public string Color { get; set; }
        public decimal? UnitPriceFrom { get; set; }
        public decimal? UnitPriceTo { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public ProductSearchCriteria()
        {
            ProductCategory = new ProductCategory();
        }

    }
}
