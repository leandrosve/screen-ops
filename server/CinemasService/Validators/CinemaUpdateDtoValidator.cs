using CinemasService.Dtos;
using CinemasService.Errors;
using FluentValidation;

namespace CinemasService.Validators
{
    public class CinemaUpdateDtoValidator : AbstractValidator<CinemaUpdateDto>
    {
        public CinemaUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
            .MinimumLength(1).WithMessage(CinemaErrors.Update.NameMinLength)
            .MaximumLength(100).WithMessage(CinemaErrors.Update.NameMaxLength);

            RuleFor(x => x.Location)
                .MinimumLength(1).WithMessage(CinemaErrors.Update.LocationMinLength)
                .MaximumLength(150).WithMessage(CinemaErrors.Update.LocationMaxLength);

            RuleFor(x => x.Description)
                .MinimumLength(1).WithMessage(CinemaErrors.Update.DescriptionMinLength)
                .MaximumLength(500).WithMessage(CinemaErrors.Update.DescriptionMaxLength);

            RuleFor(x => x.Capacity)
               .GreaterThan(0).WithMessage(CinemaErrors.Update.CapacityInvalid);

            RuleFor(x => x.Status)
              .IsInEnum().WithMessage(CinemaErrors.Update.StatusInvalid);

            RuleFor(x => x.ImageUrl)
               .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage(CinemaErrors.Create.ImageUrlInvalid)
               .When(x => x.ImageUrl != null);
        }
    }
}
