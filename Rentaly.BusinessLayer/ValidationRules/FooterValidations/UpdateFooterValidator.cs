using FluentValidation;
using Rentaly.DtoLayer.FooterDtos;

namespace Rentaly.BusinessLayer.ValidationRules.FooterValidations;

public class UpdateFooterValidator : AbstractValidator<UpdateFooterDto>
{
    public UpdateFooterValidator()
    {
        RuleFor(x => x.FooterId)
            .GreaterThan(0).WithMessage("Geçersiz ID.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Telefon boş olamaz.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Adres boş olamaz.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş olamaz.")
            .EmailAddress().WithMessage("Geçerli email giriniz.");
    }
}