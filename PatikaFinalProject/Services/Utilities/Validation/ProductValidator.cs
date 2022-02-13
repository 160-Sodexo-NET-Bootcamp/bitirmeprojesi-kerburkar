using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities.Validation
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(q => q.Name).NotNull().WithMessage("Ürün Adı Alanı Boş Olmamalı.")
                .MaximumLength(100).WithMessage("Ürün Adı 100 karakterden uzun olamaz");
            RuleFor(q => q.Description).NotNull().WithMessage("Ürün Açıklama Alanı Boş Olmamalı.")
                            .MaximumLength(500).WithMessage("Ürün Adı 500 karakterden uzun olamaz");
            RuleFor(q => q.CategoryId).NotNull().WithMessage("Ürün Kategori Alanı Boş Olmamalı.");
            RuleFor(q => q.BrandId).NotNull().WithMessage("Ürün Marka Alanı Boş Olmamalı.");
            RuleFor(q => q.ColourId).NotNull().WithMessage("Ürün Renk Alanı Boş Olmamalı.");
            RuleFor(q => q.StatusId).NotNull().WithMessage("Ürün Durum Alanı Boş Olmamalı.");
           
            //RuleFor(q => q.ProductPicture).NotNull().WithMessage("Ürün Resmi Alanı Boş Olmamalı.");
            //RuleFor(f => f.ProductPicture.Length).ExclusiveBetween(0, 400)
            //   .WithMessage($"Ürün Resmi 400KB dan büyük olamaz")
            //   .When(f => f.ProductPicture != null);
           
            //RuleFor(x => x.ProductPicture.ContentType).Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
            //    .WithMessage("Ürün Resmi yalnızca jpeg,jpg,png destekler");

            RuleFor(q => q.Price).NotNull().WithMessage("Ürün Fiyatı Alanı Boş Olmamalı.");

        }
    }
}
