using FluentValidation;
using Rentaly.DtoLayer.FooterDtos;

namespace Rentaly.BusinessLayer.ValidationRules.FooterValidations;

public class CreateFooterValidator : AbstractValidator<CreateFooterDto>
{
    public CreateFooterValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalı.")
            .MaximumLength(500).WithMessage("Açıklama çok uzun.");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
            .Matches(@"^\+?[0-9\s\-]{10,15}$")
            .WithMessage("Geçerli bir telefon numarası giriniz.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adres boş olamaz.")
            .MinimumLength(10).WithMessage("Adres en az 10 karakter olmalı.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");
    }
}