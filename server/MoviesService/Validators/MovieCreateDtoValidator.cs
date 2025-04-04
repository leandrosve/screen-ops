using FluentValidation;
using MoviesService.Dtos;
using MoviesService.Enums;
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
               .NotEmpty().WithMessage("original_title_required")
               .MaximumLength(255).WithMessage("original_title_max_length");

            RuleFor(x => x.LocalizedTitle)
                .NotEmpty().WithMessage("localized_title_required")
                .MaximumLength(255).WithMessage("localized_title_max_length");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("description_required")
                .MaximumLength(1000).WithMessage("description_max_length");

            RuleFor(x => x.Director)
                .NotEmpty().WithMessage("director_required")
                .MaximumLength(255).WithMessage("director_max_length");

            RuleFor(x => x.MainActors)
                .NotEmpty().WithMessage("main_actors_required")
                .MaximumLength(500).WithMessage("main_actors_max_length");

            RuleFor(x => x.Duration)
                .NotNull().WithMessage("duration_required")
                .GreaterThan(0).WithMessage("duration_must_be_positive");

            RuleFor(x => x.OriginalReleaseYear)
                .NotNull().WithMessage("release_year_required")
                .InclusiveBetween(1888, DateTime.UtcNow.Year + 1).WithMessage("release_year_invalid");

            RuleFor(x => x.CountryCode)
                .NotEmpty().WithMessage("country_code_required")
                .Must(x => CountryConstants.GetByCode(x) != null).WithMessage("country_code_invalid");

            RuleFor(x => x.OriginalLanguageCode)
                .NotEmpty().WithMessage("language_code_required")
                .Must(x => LanguageConstants.GetByCode(x) != null).WithMessage("language_code_invalid");

            RuleFor(x => x.Media)
                .NotNull().WithMessage("media_required");

            RuleForEach(m => m.Media).ChildRules(media =>
            {
                media.RuleFor(m => m.Url)
                    .NotEmpty().WithMessage("url_required")
                    .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).WithMessage("url_invalid");

                media.RuleFor(m => m.Type)
                    .NotEmpty().WithMessage("media_type_required")
                    .Must(type => Enum.IsDefined(typeof(MovieMediaType), type))
                    .WithMessage("media_type_invalid");
            });

            RuleFor(x => x.GenreIds)
             .NotEmpty().WithMessage("genres_required");

            RuleFor(x => x.GenreIds)
                .NotEmpty().WithMessage("genres_required")
                .Must(ids =>
                {
                    var existingIds = _genreRepository.GetAllIdsSync(); // Método síncrono
                    return ids.All(id => existingIds.Contains(id));
                })
                .WithMessage("genre_id_invalid");

        }
    }
}
