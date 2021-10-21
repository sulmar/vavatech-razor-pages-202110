using System;
using System.Collections.Generic;
using System.Text;

namespace Vavatech.RazorPages.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string BarCode { get; set; }
        public string Size { get; set; }
        public float Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

        public float FromTemperature { get; set; }
        public float ToTemperature { get; set; }

        public byte[] Photo { get; set; }


        public Product()
        {
            Tags = new List<Tag>();
        }
    }

    

   


}
