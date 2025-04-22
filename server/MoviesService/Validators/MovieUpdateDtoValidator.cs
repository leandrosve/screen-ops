using FluentValidation;
using MoviesService.Dtos;
using MoviesService.Enums;
using MoviesService.Errors;
using MoviesService.Repositories;
using MoviesService.Static;

namespace MoviesService.Validators
{
    public class MovieUpdateDtoValidator : AbstractValidator<MovieUpdateDto>
    {
        private readonly IGenreRepository _genreRepository;

        public MovieUpdateDtoValidator(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.OriginalTitle)
               .MinimumLength(1).WithMessage(MovieErrors.Update.OriginalTitleMinLength)
               .MaximumLength(255).WithMessage(MovieErrors.Update.OriginalTitleMaxLength);

            RuleFor(x => x.LocalizedTitle)
                .MinimumLength(1).WithMessage(MovieErrors.Update.LocalizedTitleMinLength)
                .MaximumLength(255).WithMessage(MovieErrors.Update.LocalizedTitleMaxLength);

            RuleFor(x => x.Description)
                .MinimumLength(1).WithMessage(MovieErrors.Update.DescriptionMinLength)
                .MaximumLength(1000).WithMessage(MovieErrors.Update.DescriptionMaxLength);

            RuleFor(x => x.Director)
                .MinimumLength(1).WithMessage(MovieErrors.Update.DescriptionMinLength)
                .MaximumLength(255).WithMessage(MovieErrors.Update.DirectorMaxLength);

            RuleFor(x => x.MainActors)
                .MinimumLength(1).WithMessage(MovieErrors.Update.MainActorsMinLength)
                .MaximumLength(500).WithMessage(MovieErrors.Update.MainActorsMaxLength);

            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage(MovieErrors.Update.DurationMustBePositive);

            RuleFor(x => x.OriginalReleaseYear)
                .InclusiveBetween(1888, DateTime.UtcNow.Year + 1).WithMessage(MovieErrors.Update.ReleaseYearInvalid);

            RuleFor(x => x.CountryCode)
                .Must(x => x == null || CountryConstants.GetByCode(x) != null).WithMessage(MovieErrors.Update.CountryCodeInvalid);

            RuleFor(x => x.OriginalLanguageCode)
                .Must(x => x == null || LanguageConstants.GetByCode(x) != null).WithMessage(MovieErrors.Update.LanguageCodeInvalid);

            RuleFor(x => x.Status)
               .IsInEnum().WithMessage(MovieErrors.Update.LanguageCodeInvalid);

            RuleFor(m => m.TrailerUrl)
               .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage(MovieErrors.Create.TrailerUrlInvalid);

            RuleFor(m => m.PosterUrl)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage(MovieErrors.Create.PosterUrlInvalid);

            RuleForEach(m => m.ExtraImageUrls).ChildRules(media =>
            {
                media.RuleFor(m => m)
                    .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage(MovieErrors.Create.ExtraImageUrlInvalid);
            }).When(x => x.ExtraImageUrls != null);

            RuleFor(x => x.GenreIds)
                .Must(ids => ids == null || ids.Count > 0).WithMessage(MovieErrors.Update.GenreIdRequired)
                .Must(ids =>
                {
                    if (ids == null) return true;
                    var existingIds = _genreRepository.GetAllIdsSync(); // Método síncrono
                    return ids.All(id => existingIds.Contains(id));
                })
                .WithMessage(MovieErrors.Update.GenreIdInvalid);
        }
    }
}
