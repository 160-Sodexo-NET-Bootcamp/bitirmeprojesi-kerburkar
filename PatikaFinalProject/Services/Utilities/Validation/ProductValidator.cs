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
                .MaximumLength(100).WithMessage("Ürün Adı 100 Karakterden Uzun Olmamalı.");
            RuleFor(q => q.Description).NotNull().WithMessage("Ürün Açıklama Alanı Boş Olmamalı.")
                            .MaximumLength(500).WithMessage("Ürün Adı 500 Karakterden Uzun Olmamalı.");
            RuleFor(q => q.CategoryId).NotNull().WithMessage("Ürün Kategori Alanı Boş Olmamalı.");
            RuleFor(q => q.BrandId).NotNull().WithMessage("Ürün Marka Alanı Boş Olmamalı.");
            RuleFor(q => q.ColourId).NotNull().WithMessage("Ürün Renk Alanı Boş Olmamalı.");
            RuleFor(q => q.StatusId).NotNull().WithMessage("Ürün Durum Alanı Boş Olmamalı.");

            RuleFor(q => q.Price).NotNull().WithMessage("Ürün Fiyatı Alanı Boş Olmamalı.");

        }
    }
}
