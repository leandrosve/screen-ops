using CinemasService.Dtos;
using CinemasService.Errors;
using FluentValidation;

namespace CinemasService.Validators
{
    public class LayoutUpdateDtoValidator : AbstractValidator<LayoutUpdateDto>
    {
        public LayoutUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
                .MinimumLength(1).WithMessage(LayoutErrors.Create.NameRequired)
                .MaximumLength(100).WithMessage(LayoutErrors.Create.NameMaxLength);

            RuleFor(x => x.Rows)
                .GreaterThan(0).WithMessage(LayoutErrors.Create.DimensionsTooSmall);

            RuleFor(x => x.Columns)
                .GreaterThan(0).WithMessage(LayoutErrors.Create.DimensionsTooSmall);

            RuleFor(x => x.Elements)
                .NotEmpty()
                .WithMessage(LayoutErrors.Create.ElementsRequired)
                .When(x => x.Elements != null);
        }
    }
}
