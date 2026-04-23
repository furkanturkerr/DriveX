using FluentValidation;
using Rentaly.DtoLayer.ServiceDtos;

namespace Rentaly.BusinessLayer.ValidationRules.ServiceValidations;

public class CreateServiceValidator : AbstractValidator<CreateServiceDto>
{
    public CreateServiceValidator()
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
            .NotEmpty().WithMessage("İkon boş olamaz.")
            .Must(BeValidIcon).WithMessage("Geçerli bir ikon yolu girin.");

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Pozisyon seçmek zorunludur.")
            .Must(x => x == "fadeInLeft" || x == "fadeInRight")
            .WithMessage("Geçersiz pozisyon seçimi.");
    }

    private bool BeValidIcon(string icon)
    {
        if (string.IsNullOrWhiteSpace(icon))
            return false;

        return icon.EndsWith(".svg") || icon.EndsWith(".png") || icon.StartsWith("/");
    }
}