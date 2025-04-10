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
    public class MovieUpdateDtoTest
    {
        private MovieUpdateDto EmptyDto() => new()
        {
            OriginalTitle = null,
            LocalizedTitle = null,
            Description = null,
            Director = null,
            MainActors = null,
            Duration = null,
            OriginalReleaseYear = null,
            CountryCode = null,
            OriginalLanguageCode = null,
            GenreIds = null,
            Media = null
        };


        private void ShouldContainError(ValidationResult result, string error) {
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().Which.ErrorMessage.Should().Be(error);
        }

        private void ShouldBeValid(ValidationResult result)
        {
            result.IsValid.Should().BeTrue();
        }

        static MovieUpdateDtoTest()
        {
            CountryConstants.Load();
            LanguageConstants.Load();
        }

        [Fact]
        public void MovieUpdateDto_ShouldFailValidation_WhenDtoIsInvalid()
        {
            MovieUpdateDto dto = EmptyDto();

            var genreRepo = Substitute.For<IGenreRepository>();
            genreRepo.GetAllIdsSync().Returns(new HashSet<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            var validator = new MovieUpdateDtoValidator(genreRepo);

            // Valid

            ShouldBeValid(validator.Validate(dto));

            // OriginalTitle
            dto.OriginalTitle = "";
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.OriginalTitleMinLength);

            dto.OriginalTitle = new string('a', 256);
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.OriginalTitleMaxLength);

            // Description
            dto = EmptyDto();
            dto.Description = "";
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.DescriptionMinLength);

            dto.Description = new string('a', 3001);
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.DescriptionMaxLength);

            // Director
            dto = EmptyDto();
            dto.Director = "";
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.DescriptionMinLength);

            dto.Director = new string('a', 256);
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.DirectorMaxLength);

            // Duration
            dto = EmptyDto();
            dto.Duration = 0;
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.DurationMustBePositive);

            // OriginalReleaseYear
            dto = EmptyDto();
            dto.OriginalReleaseYear = 1700;
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.ReleaseYearInvalid);

            dto.OriginalReleaseYear = DateTime.UtcNow.Year + 1;
            ShouldBeValid(validator.Validate(dto));

            dto.OriginalReleaseYear = DateTime.UtcNow.Year + 2;
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.ReleaseYearInvalid);

            // CountryCode
            dto = EmptyDto();

            dto.CountryCode = "";
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.CountryCodeInvalid);

            dto.CountryCode = "ARG";
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.CountryCodeInvalid);

            dto = EmptyDto();
            dto.CountryCode = "AR";
            ShouldBeValid(validator.Validate(dto));

            dto = EmptyDto();
            dto.CountryCode = "ar";
            ShouldContainError(validator.Validate(dto),MovieErrors.Update.CountryCodeInvalid);

            // OriginalLanguageCode
            dto = EmptyDto();

            dto.OriginalLanguageCode = "";
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.LanguageCodeInvalid);

            dto.OriginalLanguageCode = "SPA";
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.LanguageCodeInvalid);

            dto.OriginalLanguageCode = "ES";
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.LanguageCodeInvalid);

            dto.OriginalLanguageCode = "es";
            ShouldBeValid(validator.Validate(dto));

            // GenreIds
            dto = EmptyDto();

            dto.GenreIds = new List<int>();
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.GenreIdRequired);

            dto.GenreIds = new List<int> { 99999 };
            ShouldContainError(validator.Validate(dto), MovieErrors.Update.GenreIdInvalid);

            dto.GenreIds = new List<int> { 1, 2, 5, 7 };
            ShouldBeValid(validator.Validate(dto));
        }
    }
}
