using FluentValidation;
using Rentaly.DtoLayer.TestimonialDtos;

namespace Rentaly.BusinessLayer.ValidationRules.TestimonialValidations;

public class CreateTestimonialValidator : AbstractValidator<CreateTestimonialDto>
{
    public CreateTestimonialValidator()
    {
        RuleFor(x => x.NameSurname)
            .NotEmpty().WithMessage("Ad soyad boş olamaz.")
            .MinimumLength(3).WithMessage("Ad soyad çok kısa.")
            .MaximumLength(60).WithMessage("Ad soyad çok uzun.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Unvan boş olamaz.")
            .MinimumLength(3).WithMessage("Unvan çok kısa.")
            .MaximumLength(80).WithMessage("Unvan çok uzun.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Yorum boş olamaz.")
            .MinimumLength(10).WithMessage("Yorum en az 10 karakter olmalı.")
            .MaximumLength(500).WithMessage("Yorum çok uzun.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Görsel boş olamaz.")
            .Must(BeValidImage).WithMessage("Geçerli bir görsel yolu gir.");
    }

    private bool BeValidImage(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        return url.EndsWith(".jpg") ||
               url.EndsWith(".png") ||
               url.EndsWith(".webp") ||
               url.StartsWith("/");
    }
}