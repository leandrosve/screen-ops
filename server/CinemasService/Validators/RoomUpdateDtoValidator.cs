using CinemasService.Dtos;
using CinemasService.Errors;
using FluentValidation;

namespace CinemasService.Validators
{
    public class RoomUpdateDtoValidator : AbstractValidator<RoomUpdateDto>
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

            RuleFor(x => x.LayoutId).Must(id => id != Guid.Empty)
              .WithMessage(RoomErrors.Update.LayoutIdRequired)
              .When(x => x.LayoutId != null);
        }
    }
}
