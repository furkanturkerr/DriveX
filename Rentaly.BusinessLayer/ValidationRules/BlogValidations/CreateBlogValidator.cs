using FluentValidation;
using Rentaly.DtoLayer.BlogDtos;

namespace Rentaly.BusinessLayer.ValidationRules.BlogValidations;

public class CreateBlogValidator : AbstractValidator<CreateBlogDto>
{
    public CreateBlogValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.")
            .MinimumLength(5).WithMessage("Başlık en az 5 karakter olmalı.")
            .MaximumLength(150).WithMessage("Başlık çok uzun.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.")
            .MinimumLength(20).WithMessage("Açıklama en az 20 karakter olmalı.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Görsel yolu boş olamaz.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Kategori seçmelisiniz.");
    }
}