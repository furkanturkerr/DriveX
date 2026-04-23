using FluentValidation;
using Rentaly.DtoLayer.FaqDtos;

namespace Rentaly.BusinessLayer.ValidationRules.FaqValidations;

public class UpdateFaqValidator : AbstractValidator<UpdateFaqDto>
{
    public UpdateFaqValidator()
    {
        RuleFor(x => x.FaqId)
            .GreaterThan(0).WithMessage("Geçersiz ID.");

        RuleFor(x => x.Question)
            .NotEmpty().WithMessage("Soru boş olamaz.");

        RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("Cevap boş olamaz.");
    }
}