using FluentValidation;
using MoviesService.Dtos;
using MoviesService.Enums;
using MoviesService.Errors;
using MoviesService.Repositories;
using MoviesService.Static;

namespace MoviesService.Validators
{
    public class MovieCreateDtoValidator : AbstractValidator<MovieCreateDto>
    {
        private readonly IGenreRepository _genreRepository;

        public MovieCreateDtoValidator(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.OriginalTitle)
               .NotEmpty().WithMessage(MovieErrors.Create.OriginalTitleRequired)
               .MaximumLength(255).WithMessage(MovieErrors.Create.OriginalTitleMaxLength);

            RuleFor(x => x.LocalizedTitle)
                .NotEmpty().WithMessage(MovieErrors.Create.LocalizedTitleRequired)
                .MaximumLength(255).WithMessage(MovieErrors.Create.LocalizedTitleMaxLength);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(MovieErrors.Create.DescriptionRequired)
                .MaximumLength(1000).WithMessage(MovieErrors.Create.DescriptionMaxLength);

            RuleFor(x => x.Director)
                .NotEmpty().WithMessage(MovieErrors.Create.DirectorRequired)
                .MaximumLength(255).WithMessage(MovieErrors.Create.DirectorMaxLength);

            RuleFor(x => x.MainActors)
                .NotEmpty().WithMessage(MovieErrors.Create.MainActorsRequired)
                .MaximumLength(500).WithMessage(MovieErrors.Create.MainActorsMaxLength);

            RuleFor(x => x.Duration)
                .NotNull().WithMessage(MovieErrors.Create.DurationRequired)
                .GreaterThan(0).WithMessage(MovieErrors.Create.DurationMustBePositive);

            RuleFor(x => x.OriginalReleaseYear)
                .NotNull().WithMessage(MovieErrors.Create.ReleaseYearRequired)
                .InclusiveBetween(1888, DateTime.UtcNow.Year + 1).WithMessage(MovieErrors.Create.ReleaseYearInvalid);

            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage(MovieErrors.Create.CountryCodeRequired)
                .Must(x => CountryConstants.GetByCode(x) != null).WithMessage(MovieErrors.Create.CountryCodeInvalid);

            RuleFor(x => x.OriginalLanguageCode)
                .NotEmpty().WithMessage(MovieErrors.Create.LanguageCodeRequired)
                .Must(x => LanguageConstants.GetByCode(x) != null).WithMessage(MovieErrors.Create.LanguageCodeInvalid);


           RuleFor(m => m.TrailerUrl)
                .NotEmpty().WithMessage(MovieErrors.Create.TrailerUrlRequired)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage(MovieErrors.Create.TrailerUrlInvalid);

            RuleFor(m => m.PosterUrl)
                .NotEmpty().WithMessage(MovieErrors.Create.PosterUrlRequired)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage(MovieErrors.Create.PosterUrlInvalid);

            RuleForEach(m => m.ExtraImageUrls).ChildRules(media =>
            {
                media.RuleFor(m => m)
                    .NotEmpty().WithMessage(MovieErrors.Create.ExtraImageUrlInvalid)
                    .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage(MovieErrors.Create.ExtraImageUrlInvalid);
            });

            RuleFor(x => x.GenreIds)
             .NotEmpty().WithMessage(MovieErrors.Create.GenresRequired);

            RuleFor(x => x.GenreIds)
                .NotEmpty().WithMessage(MovieErrors.Create.GenresRequired)
                .Must(ids =>
                {
                    var existingIds = _genreRepository.GetAllIdsSync(); // Método síncrono
                    return ids.All(id => existingIds.Contains(id));
                })
                .WithMessage(MovieErrors.Create.GenreIdInvalid);

        }
    }
}
