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
    }
}
