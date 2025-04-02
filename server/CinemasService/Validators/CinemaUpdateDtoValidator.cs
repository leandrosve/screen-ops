using CinemasService.Dtos;
using FluentValidation;

namespace CinemasService.Validators
{
    public class CinemaUpdateDtoValidator : AbstractValidator<CinemaUpdateDto>
    {
        public CinemaUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("name_max_length");

            RuleFor(x => x.Location)
                .MaximumLength(150).WithMessage("location_max_length");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("description_max_length");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("capacity_invalid");
        }
    }
}
