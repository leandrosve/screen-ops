namespace MoviesService.Errors
{
    public class MovieErrors
    {

        public static class Create
        {

            public const string OriginalTitleRequired = "original_title_required";
            public const string OriginalTitleMaxLength = "original_title_max_length";

            public const string LocalizedTitleRequired = "localized_title_required";
            public const string LocalizedTitleMaxLength = "localized_title_max_length";

            public const string DescriptionRequired = "description_required";
            public const string DescriptionMaxLength = "description_max_length";

            public const string DirectorRequired = "director_required";
            public const string DirectorMaxLength = "director_max_length";

            public const string MainActorsRequired = "main_actors_required";
            public const string MainActorsMaxLength = "main_actors_max_length";

            public const string DurationRequired = "duration_required";
            public const string DurationMustBePositive = "duration_must_be_positive";

            public const string ReleaseYearRequired = "release_year_required";
            public const string ReleaseYearInvalid = "release_year_invalid";

            public const string CountryCodeRequired = "country_code_required";
            public const string CountryCodeInvalid = "country_code_invalid";

            public const string LanguageCodeRequired = "language_code_required";
            public const string LanguageCodeInvalid = "language_code_invalid";

            public const string MediaRequired = "media_required";
            public const string MediaUrlRequired = "media_url_required";
            public const string MediaUrlInvalid = "media_url_invalid";
            public const string MediaTypeRequired = "media_type_required";
            public const string MediaTypeInvalid = "media_type_invalid";

            public const string GenresRequired = "genres_required";
            public const string GenreIdInvalid = "genre_id_invalid";
        }

        public static class Update
        {
            public const string MovieNotFound = "movie_not_found";

            public const string OriginalTitleMinLength = "original_title_min_length";
            public const string OriginalTitleMaxLength = "original_title_max_length";

            public const string LocalizedTitleMinLength = "localized_title_min_length";
            public const string LocalizedTitleMaxLength = "localized_title_max_length";

            public const string DescriptionMinLength = "description_min_length";
            public const string DescriptionMaxLength = "description_max_length";

            public const string DirectorMinLength = "director_min_length";
            public const string DirectorMaxLength = "director_max_length";

            public const string MainActorsMinLength = "main_actors_min_length";
            public const string MainActorsMaxLength = "main_actors_max_length";

            public const string DurationMustBePositive = "duration_must_be_positive";

            public const string ReleaseYearInvalid = "release_year_invalid";

            public const string CountryCodeInvalid = "country_code_invalid";
            public const string LanguageCodeInvalid = "language_code_invalid";

            public const string MediaUrlRequired = "media_url_required";
            public const string MediaUrlInvalid = "media_url_invalid";

            public const string MediaTypeRequired = "media_type_required";
            public const string MediaTypeInvalid = "media_type_invalid";

            public const string GenreIdRequired = "genre_id_required";
            public const string GenreIdInvalid = "genre_id_invalid";
        }

        public static class Get
        {
            public const string MovieNotFound = "movie_not_found";
            public const string SearchTermTooLong = "search_term_too_long";
            public const string PaginationRequired = "pagination_required";
            public const string PageInvalid = "page_invalid";
            public const string PageSizeOutOfRange = "page_size_out_of_range";
        }

        public static class Delete
        {
            public const string MovieNotFound = "movie_not_found";
        }
    }
}