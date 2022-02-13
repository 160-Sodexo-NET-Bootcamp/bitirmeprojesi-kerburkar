using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities.Validation
{
    internal class ProductImageValidator : AbstractValidator<ProductImageDto>
    {
        public ProductImageValidator()
        {


            RuleFor(q => q.ProductImage).NotNull().WithMessage("Ürün Resmi Alanı Boş Olmamalı.");
            RuleFor(f => f.ProductImage.Length).ExclusiveBetween(0, 400)
               .WithMessage($"Ürün Resmi 400KB dan büyük olamaz")
               .When(f => f.ProductImage != null);

            RuleFor(x => x.ProductImage.ContentType).Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage("Ürün Resmi yalnızca jpeg,jpg,png destekler");

            RuleFor(q => q.ProductId).NotNull().WithMessage("Ürün Id Alanı Boş Olmamalı.");

        }
    }
}
