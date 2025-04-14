using FluentValidation;
using ScreeningsService.Dtos;
using ScreeningsService.Errors;

namespace ScreeningsService.Validators
{
    public class ScreeningScheduleSearchFiltersDtoValidator : AbstractValidator<ScreeningScheduleSearchFiltersDto>
    {
        public ScreeningScheduleSearchFiltersDtoValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ToDate)
                        .GreaterThanOrEqualTo(x => x.FromDate)
                        .When(x => x.FromDate.HasValue && x.ToDate.HasValue)
                        .WithMessage(ScreeningScheduleErrors.GetByFilters.ToDateAfterFromDate);

            RuleForEach(x => x.Status)
                .IsInEnum()
                .When(x => x.Status != null).WithMessage(ScreeningScheduleErrors.GetByFilters.StatusInvalid);
        }

    };

}
