using CinemasService.Dtos;
using CinemasService.Errors;
using FluentValidation;

namespace CinemasService.Validators
{
    public class RoomUpdateDtoValidator : AbstractValidator<RoomCreateDto>
    {
        public RoomUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
                .MinimumLength(1).WithMessage(RoomErrors.Update.NameMinLength)
                .MaximumLength(100).WithMessage(RoomErrors.Update.NameMaxLength);
            RuleFor(x => x.Description)
                .MinimumLength(1).WithMessage(RoomErrors.Update.DescriptionMinLength)
                .MaximumLength(100).WithMessage(RoomErrors.Update.DescriptionMaxLength);
        }
    }
}
