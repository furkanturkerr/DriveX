using FluentValidation;
using Rentaly.DtoLayer.FaqDtos;

namespace Rentaly.BusinessLayer.ValidationRules.FaqValidations;

public class CreateFaqValidator : AbstractValidator<CreateFaqDto>
{
    public CreateFaqValidator()
    {
        RuleFor(x => x.Question)
            .NotEmpty().WithMessage("Soru boş olamaz.")
            .MinimumLength(5).WithMessage("Soru en az 5 karakter olmalı.")
            .MaximumLength(200).WithMessage("Soru çok uzun.");

        RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("Cevap boş olamaz.")
            .MinimumLength(10).WithMessage("Cevap en az 10 karakter olmalı.")
            .MaximumLength(1000).WithMessage("Cevap çok uzun.");
    }
}