using CinemasService.Dtos;
using FluentValidation;

namespace CinemasService.Validators
{
    public class RoomUpdateDtoValidator : AbstractValidator<RoomCreateDto>
    {
        public RoomUpdateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
                .MinimumLength(1).WithMessage("name_min_length")
                .MaximumLength(100).WithMessage("name_max_length");
            RuleFor(x => x.Description)
                .MinimumLength(1).WithMessage("description_min_length")
                .MaximumLength(100).WithMessage("description_max_length");
        }
    }
}
