using FluentValidation;
using Rentaly.DtoLayer.ServiceDtos;

namespace Rentaly.BusinessLayer.ValidationRules.ServiceValidations;

public class UpdateServiceValidator : AbstractValidator<UpdateServiceDto>
{
    public UpdateServiceValidator()
    {
        RuleFor(x => x.ServiceId)
            .GreaterThan(0).WithMessage("Geçersiz ID.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.");

        RuleFor(x => x.IconUrl)
            .NotEmpty().WithMessage("İkon boş olamaz.");

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Pozisyon boş olamaz.");
    }
}