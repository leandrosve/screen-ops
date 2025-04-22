using FluentValidation;
using MoviesService.Dtos;
using MoviesService.Enums;
using MoviesService.Errors;

namespace MoviesService.Validators
{
    public class MovieFiltersDtoValidator : AbstractValidator<MovieFiltersDto>
    {
        private readonly ISet<int> _validStatus = Enum.GetValues<MovieStatusEnum>()
                                                    .Select(e => (int)e)
                                                    .ToHashSet();
        public MovieFiltersDtoValidator()
        {
            RuleFor(x => x.SearchTerm)
                .MaximumLength(100).WithMessage(MovieErrors.Get.SearchTermTooLong);

            RuleFor(x => x.Pagination)
                .NotNull().WithMessage(MovieErrors.Get.PaginationRequired);

            RuleFor(x => x.Pagination.Page)
           .GreaterThanOrEqualTo(1).WithMessage(MovieErrors.Get.PageInvalid);

            RuleFor(x => x.Pagination.PageSize)
                .InclusiveBetween(1, 100).WithMessage(MovieErrors.Get.PageSizeOutOfRange);

            RuleForEach(x => x.Status)
               .Must(x => _validStatus.Contains(x)).WithMessage(MovieErrors.Get.StatusInvalid);
        }
    }
}
