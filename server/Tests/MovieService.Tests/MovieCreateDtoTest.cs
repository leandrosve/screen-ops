using FluentAssertions;
using FluentValidation.Results;
using MoviesService.Dtos;
using MoviesService.Errors;
using MoviesService.Repositories;
using MoviesService.Static;
using MoviesService.Validators;
using NSubstitute;

namespace MovieService.Tests
{
    public class MovieCreateDtoTest
    {
        private MovieCreateDto ValidDto() => new()
        {
            OriginalTitle = "Valid Title",
            LocalizedTitle = "Valid Title",
            Description = "Valid description",
            Director = "Someone",
            MainActors = "Actors",
            Duration = 100,
            OriginalReleaseYear = 2024,
            CountryCode = "US",
            OriginalLanguageCode = "en",
            GenreIds = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
            Media = new List<MovieMediaCreateDto>()
        };


        private void ShouldContainError(ValidationResult result, string error) {
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be(error);
        }

        private void ShouldBeValid(ValidationResult result)
        {
            result.IsValid.Should().BeTrue();
        }

        static MovieCreateDtoTest()
        {
            CountryConstants.Load();
            LanguageConstants.Load();
        }

        [Fact]
        public void MovieCreateDto_ShouldFailValidation_WhenDtoIsInvalid()
        {
            MovieCreateDto dto = ValidDto();

            var genreRepo = Substitute.For<IGenreRepository>();
            genreRepo.GetAllIdsSync().Returns(new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            var validator = new MovieCreateDtoValidator(genreRepo);

            // Valid

            ShouldBeValid(validator.Validate(dto));

            // OriginalTitle
            dto.OriginalTitle = null!;
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.OriginalTitleRequired);

            dto.OriginalTitle = "";
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.OriginalTitleRequired);

            dto.OriginalTitle = new string('a', 256);
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.OriginalTitleMaxLength);

            // Description
            dto = ValidDto();
            dto.Description = "";
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.DescriptionRequired);

            dto.Description = new string('a', 3001);
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.DescriptionMaxLength);

            // Director
            dto = ValidDto();
            dto.Director = "";
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.DirectorRequired);

            dto.Director = new string('a', 256);
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.DirectorMaxLength);

            // Duration
            dto = ValidDto();
            dto.Duration = 0;
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.DurationMustBePositive);

            // OriginalReleaseYear
            dto = ValidDto();
            dto.OriginalReleaseYear = 1700;
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.ReleaseYearInvalid);

            dto.OriginalReleaseYear = DateTime.UtcNow.Year + 1;
            ShouldBeValid(validator.Validate(dto));

            dto.OriginalReleaseYear = DateTime.UtcNow.Year + 2;
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.ReleaseYearInvalid);

            // CountryCode
            dto = ValidDto();

            dto.CountryCode = null!;
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.CountryCodeRequired);

            dto.CountryCode = "";
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.CountryCodeRequired);

            dto.CountryCode = "ARG";
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.CountryCodeInvalid);

            dto = ValidDto();
            dto.CountryCode = "AR";
            ShouldBeValid(validator.Validate(dto));

            dto = ValidDto();
            dto.CountryCode = "ar";
            ShouldContainError(validator.Validate(dto),MovieErrors.Create.CountryCodeInvalid);

            // OriginalLanguageCode
            dto = ValidDto();

            dto.OriginalLanguageCode = null!;
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.LanguageCodeRequired);

            dto.OriginalLanguageCode = "";
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.LanguageCodeRequired);

            dto.OriginalLanguageCode = "SPA";
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.LanguageCodeInvalid);

            dto.OriginalLanguageCode = "ES";
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.LanguageCodeInvalid);

            dto.OriginalLanguageCode = "es";
            ShouldBeValid(validator.Validate(dto));

            // GenreIds
            dto = ValidDto();

            dto.GenreIds = null!;
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.GenresRequired);

            dto.GenreIds = new List<int>();
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.GenresRequired);

            dto.GenreIds = new List<int> { 99999 };
            ShouldContainError(validator.Validate(dto), MovieErrors.Create.GenreIdInvalid);

            dto.GenreIds = new List<int> { 1, 2, 5, 7 };
            ShouldBeValid(validator.Validate(dto));
        }
    }
}
