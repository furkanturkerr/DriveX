using FluentValidation;
using Rentaly.DtoLayer.TestimonialDtos;

namespace Rentaly.BusinessLayer.ValidationRules.TestimonialValidations;

public class UpdateTestimonialValidator : AbstractValidator<UpdateTestimonialDto>
{
    public UpdateTestimonialValidator()
    {
        RuleFor(x => x.TestimonialId)
            .GreaterThan(0).WithMessage("Geçersiz ID.");

        RuleFor(x => x.NameSurname)
            .NotEmpty().WithMessage("Ad soyad boş olamaz.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Unvan boş olamaz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Yorum boş olamaz.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Görsel boş olamaz.");
    }
}