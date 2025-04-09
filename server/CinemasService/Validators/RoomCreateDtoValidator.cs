using CinemasService.Dtos;
using FluentValidation;

namespace CinemasService.Validators
{
    public class RoomCreateDtoValidator : AbstractValidator<RoomCreateDto>
    {
        public RoomCreateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("name_required")
                .MaximumLength(100).WithMessage("name_max_length");
            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("description_max_length");
            RuleFor(x => x.CinemaId).Must(id => id != Guid.Empty)
                .WithMessage("cinema_id_required");
        }
    }
}
