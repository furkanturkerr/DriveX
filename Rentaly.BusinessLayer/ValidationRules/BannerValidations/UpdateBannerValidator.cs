using FluentValidation;
using Rentaly.DtoLayer.BannerDtos;

public class UpdateBannerValidator : AbstractValidator<UpdateBannerDto>
{
    public UpdateBannerValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık boş olamaz.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıklama boş olamaz.");

        RuleFor(x => x.IconUrl)
            .NotEmpty().WithMessage("İkon boş olamaz.");
    }
}