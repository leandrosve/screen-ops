using CinemasService.Dtos;
using FluentValidation;

namespace CinemasService.Validators
{
    public class CinemaCreateDtoValidator : AbstractValidator<CinemaCreateDto>
    {
        public CinemaCreateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("name_required")
                .MaximumLength(100).WithMessage("name_max_length");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("location_required")
                .MaximumLength(150).WithMessage("location_max_length");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("description_required")
                .MaximumLength(500).WithMessage("description_max_length");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("capacity_invalid");
        }
    }
}
