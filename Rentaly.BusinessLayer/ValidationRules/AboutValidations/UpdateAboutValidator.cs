using FluentValidation;
using Rentaly.DtoLayer.AboutDtos;

namespace Rentaly.BusinessLayer.ValidationRules.AboutValidations;

public class UpdateAboutValidator : AbstractValidator<UpdateAboutDto>
{
    public UpdateAboutValidator()
    {
        RuleFor(x => x.AboutId)
            .GreaterThan(0).WithMessage("Geçersiz ID.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MinimumLength(5).WithMessage("Başlık en az 5 karakter olmalı.")
            .MaximumLength(80).WithMessage("Başlık en fazla 80 karakter olabilir.");

        RuleFor(x => x.TitleGreen)
            .NotEmpty().WithMessage("Yeşil başlık boş olamaz.")
            .MinimumLength(2).WithMessage("Yeşil başlık en az 2 karakter olmalı.")
            .MaximumLength(50).WithMessage("Yeşil başlık en fazla 50 karakter olabilir.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MinimumLength(20).WithMessage("Açıklama en az 20 karakter olmalı.")
            .MaximumLength(1000).WithMessage("Açıklama en fazla 1000 karakter olabilir.");

        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Görsel boş olamaz.");
    }
}