using CinemasService.Dtos;
using CinemasService.Errors;
using FluentValidation;

namespace CinemasService.Validators
{
    public class CinemaCreateDtoValidator : AbstractValidator<CinemaCreateDto>
    {
        public CinemaCreateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(CinemaErrors.Create.NameRequired)
                .MaximumLength(100).WithMessage(CinemaErrors.Create.NameMaxLength);

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage(CinemaErrors.Create.LocationRequired)
                .MaximumLength(150).WithMessage(CinemaErrors.Create.LocationMaxLength);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(CinemaErrors.Create.DescriptionRequired)
                .MaximumLength(500).WithMessage(CinemaErrors.Create.DescriptionMaxLength);

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage(CinemaErrors.Create.CapacityInvalid);

            RuleFor(x => x.ImageUrl)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage(CinemaErrors.Create.ImageUrlInvalid)
                .When(x => x.ImageUrl != null);
                
        }
    }
}
