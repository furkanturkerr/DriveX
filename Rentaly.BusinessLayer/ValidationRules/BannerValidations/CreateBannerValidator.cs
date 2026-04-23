using FluentValidation;
using Rentaly.DtoLayer.BannerDtos;

namespace Rentaly.BusinessLayer.ValidationRules.BannerValidations;

public class CreateBannerValidator : AbstractValidator<CreateBannerDto>
{
    public CreateBannerValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MinimumLength(3).WithMessage("Başlık en az 3 karakter olmalı.")
            .MaximumLength(60).WithMessage("Başlık en fazla 60 karakter olabilir.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MinimumLength(10).WithMessage("Açıklama en az 10 karakter olmalı.")
            .MaximumLength(300).WithMessage("Açıklama çok uzun.");

        RuleFor(x => x.IconUrl)
            .NotEmpty().WithMessage("İkon boş olamaz.");
    }
}