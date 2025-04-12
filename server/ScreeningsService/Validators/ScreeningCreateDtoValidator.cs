using FluentValidation;
using ScreeningsService.Dtos;
using ScreeningsService.Enums;
using ScreeningsService.Errors;

namespace ScreeningsService.Validators
{
    public class ScreeningCreateDtoValidator : AbstractValidator<ScreeningCreateDto>
    {
        public ScreeningCreateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.MovieId)
                .NotEmpty().WithMessage(ScreeningErrors.Create.MovieIdRequired);

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage(ScreeningErrors.Create.RoomIdRequired);

            RuleFor(x => x.StartTime)
                .LessThan(x => x.EndTime).WithMessage(ScreeningErrors.Create.StartTimeMustBeBeforeEndTime);

            RuleFor(x => x.EndTime)
                .GreaterThan(x => x.StartTime).WithMessage(ScreeningErrors.Create.EndTimeMustBeAfterStartTime);

            RuleForEach(x => x.Features)
             .IsInEnum()
             .WithMessage(ScreeningErrors.Create.FeatureInvalid);
        }
    }
}
