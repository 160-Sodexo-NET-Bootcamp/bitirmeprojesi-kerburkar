using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Utilities.Validation
{
    public class RegisterValidator:AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(q => q.Email).EmailAddress().WithMessage("Geçersiz Email Adresi.").NotNull().WithMessage("Email Alanı Boş Olmamalı.");
            RuleFor(q => q.Password).MaximumLength(20).WithMessage("Şifre 20 Karakterden Büyük Olmamalı.").MinimumLength(8).WithMessage("Şifre 8 Karakterden Küçük Olmamalı.").NotNull().WithMessage("Şifre Alanı Boş Olmamalı.");
        }
    }
}
