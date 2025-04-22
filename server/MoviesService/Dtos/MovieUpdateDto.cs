using MoviesService.Enums;

namespace MoviesService.Dtos
{
    public class MovieUpdateDto
    {
        public string? OriginalTitle { get; set; }
        public string? LocalizedTitle { get; set; }
        public string? Description { get; set; }
        public string? Director { get; set; }
        public string? MainActors { get; set; }

        public int? Duration { get; set; }
        public int? OriginalReleaseYear { get; set; }

        public ICollection<int>? GenreIds { get; set; }
        public string? TrailerUrl { get; set; }
        public string? PosterUrl { get; set; }
        public ICollection<string>? ExtraImageUrls { get; set; }

        public string? CountryCode { get; set; }
        public string? OriginalLanguageCode { get; set; }
        public MovieStatusEnum? Status { get; set; }

        public bool ForceUpdate { get; set; } = false;
    }
}