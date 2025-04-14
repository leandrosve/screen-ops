using FluentValidation;
using ScreeningsService.Dtos;
using ScreeningsService.Errors;

namespace ScreeningsService.Validators
{
    public class ScreeningScheduleCreateDtoValidator : AbstractValidator<ScreeningScheduleCreateDto>
    {
        public ScreeningScheduleCreateDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.MovieId)
                .NotEmpty().WithMessage(ScreeningScheduleErrors.Create.MovieIdRequired);

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage(ScreeningScheduleErrors.Create.RoomIdRequired);

            RuleFor(x => x.StartDate)
               .NotEmpty().WithMessage(ScreeningScheduleErrors.Create.StartDateRequired)
               .GreaterThanOrEqualTo(x => DateOnly.FromDateTime(DateTime.Now)).WithMessage(ScreeningScheduleErrors.Create.StartDateBustBeTodayOrFuture);

            RuleFor(x => x.EndDate)
              .NotEmpty().WithMessage(ScreeningScheduleErrors.Create.EndDateRequired)
              .GreaterThanOrEqualTo(x => x.StartDate).WithMessage(ScreeningScheduleErrors.Create.EndDateBustBeAfterStartDate);

            RuleFor(x => x.Times)
                .NotEmpty().WithMessage(ScreeningScheduleErrors.Create.TimesRequired);

            RuleForEach(x => x.Times)
                .Must(x => BeValidTime(x.Start)).WithMessage(ScreeningScheduleErrors.Create.StartTimeInvalid)
                .Must(x => BeValidTime(x.End)).WithMessage(ScreeningScheduleErrors.Create.EndTimeInvalid);

            RuleFor(x => x.DaysOfWeek)
             .NotEmpty().WithMessage(ScreeningScheduleErrors.Create.DaysOfWeekRequired)
             .Must(x => !x.Any(v => ((int)v < 0) || ((int)v > 6))).WithMessage(ScreeningScheduleErrors.Create.DayOfWeekInvalid);

            RuleForEach(x => x.Features)
             .IsInEnum()
             .WithMessage(ScreeningErrors.Create.FeatureInvalid);
        }

        private static bool BeValidTime(TimeOnly time)
        {
            // Ejemplo: validar que esté dentro del horario comercial
            return time >= new TimeOnly(8, 0) && time <= new TimeOnly(23, 59);
        }
    };

}
