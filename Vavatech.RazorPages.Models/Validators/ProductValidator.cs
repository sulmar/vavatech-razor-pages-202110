using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vavatech.RazorPages.Models.Validators
{
    // Install-Package FluentValidation
    public class ProductValidator : AbstractValidator<Product>
    {
        // private readonly IProductRepository productRepository;

        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(3, 20);
            RuleFor(p => p.BarCode).Must(IsValidBarCode).WithMessage("Błędny kod kreskowy");
            RuleFor(p => p.FromTemperature).LessThanOrEqualTo(p => p.ToTemperature);            
        }

        private bool IsValidBarCode(string barcode)
        {
            // TODO: verify checksum
            return barcode.EndsWith("9");
        }
    }
}
