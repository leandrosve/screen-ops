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
        public ICollection<MovieMediaDto>? Media { get; set; }

        public string? CountryCode { get; set; }
        public string? OriginalLanguageCode { get; set; }
    }
}