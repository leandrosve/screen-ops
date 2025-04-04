namespace MoviesService.Dtos
{
    public class MovieDto
    {
        public Guid Id { get; set; }
        public string OriginalTitle { get; set; } = "";
        public string LocalizedTitle { get; set; } = "";
        public string Description { get; set; } = "";
        public string Director { get; set; } = "";
        public string MainActors { get; set; } = "";
        public int Duration { get; set; }
        public int OriginalReleaseYear { get; set; }

        public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();
        public required CountryDto Country { get; set; }
        public required LanguageDto OriginalLanguage { get; set; }
        public ICollection<MovieMediaDto> Media { get; set; } = new List<MovieMediaDto>();

        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
