using CinemasService.Dtos;
using FluentValidation;

namespace CinemasService.Validators
{
    public class LayoutCreateDtoValidator : AbstractValidator<LayoutCreateDto>
    {
        public LayoutCreateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("name_required")
                .MaximumLength(100).WithMessage("name_max_length");
            RuleFor(x => x.Elements)
                .NotEmpty()
                .WithMessage("elements_requried");
        }
    }
}
