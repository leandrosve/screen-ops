using CinemasService.Dtos;
using CinemasService.Errors;
using FluentValidation;

namespace CinemasService.Validators
{
    public class RoomCreateDtoValidator : AbstractValidator<RoomCreateDto>
    {
        public RoomCreateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(RoomErrors.Create.NameRequired)
                .MaximumLength(100).WithMessage(RoomErrors.Create.NameMaxLength);
            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage(RoomErrors.Create.DescriptionMaxLength);
            RuleFor(x => x.CinemaId).Must(id => id != Guid.Empty)
                .WithMessage(RoomErrors.Create.CinemaIdRequired);
            RuleFor(x => x.LayoutId).Must(id => id != Guid.Empty)
               .WithMessage(RoomErrors.Create.LayoutIdRequired);
        }
    }
}
