using FluentValidation;
using Rentaly.DtoLayer.BlogDtos;

namespace Rentaly.BusinessLayer.ValidationRules.BlogValidations;

public class UpdateBlogValidator : AbstractValidator<UpdateBlogDto>
{
    public UpdateBlogValidator()
    {
        RuleFor(x => x.BlogId)
            .GreaterThan(0).WithMessage("Geçersiz blog ID.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Görsel boş olamaz.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Kategori seçmelisiniz.");
    }
}