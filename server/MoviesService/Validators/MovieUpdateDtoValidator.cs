using FluentValidation;
using MoviesService.Dtos;
using MoviesService.Enums;
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
               .MinimumLength(1).WithMessage("original_title_min_length")
               .MaximumLength(255).WithMessage("original_title_max_length");

            RuleFor(x => x.LocalizedTitle)
                .MinimumLength(1).WithMessage("localized_title_min_length")
                .MaximumLength(255).WithMessage("localized_title_max_length");

            RuleFor(x => x.Description)
                .MinimumLength(1).WithMessage("description_min_length")
                .MaximumLength(1000).WithMessage("description_max_length");

            RuleFor(x => x.Director)
                .MinimumLength(1).WithMessage("director_min_length")
                .MaximumLength(255).WithMessage("director_max_length");

            RuleFor(x => x.MainActors)
                .MinimumLength(1).WithMessage("main_actors_min_length")
                .MaximumLength(500).WithMessage("main_actors_max_length");

            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage("duration_must_be_positive");

            RuleFor(x => x.OriginalReleaseYear)
                .InclusiveBetween(1888, DateTime.UtcNow.Year + 1).WithMessage("release_year_invalid");

            RuleFor(x => x.CountryCode)
                .Must(x => x == null || CountryConstants.GetByCode(x) != null).WithMessage("country_code_invalid");

            RuleFor(x => x.OriginalLanguageCode)
                .Must(x => x == null || LanguageConstants.GetByCode(x) != null).WithMessage("language_code_invalid");

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
                .Must(ids => ids == null || ids.Count > 0).WithMessage("genre_id_required")
                .Must(ids =>
                {
                    if (ids == null) return true;
                    var existingIds = _genreRepository.GetAllIdsSync(); // Método síncrono
                    return ids.All(id => existingIds.Contains(id));
                })
                .WithMessage("genre_id_invalid");


        }
    }
}
