namespace MoviesService.Dtos
{
    public class MovieCreateDto
    {
        public string OriginalTitle { get; set; } = "";
        public string LocalizedTitle { get; set; } = "";
        public string Description { get; set; } = "";
        public string Director { get; set; } = "";
        public string MainActors { get; set; } = "";

        public int Duration { get; set; }
        public int OriginalReleaseYear { get; set; }

        public ICollection<int> GenreIds { get; set; } = new List<int>();
        public ICollection<MovieMediaCreateDto> Media { get; set; } = new List<MovieMediaCreateDto>();

        public string CountryCode { get; set; } = "";
        public string OriginalLanguageCode { get; set; } = "";
    }

    public class MovieMediaCreateDto
    {
        public required string Type { get; set; }
        public required string Url { get; set; }
    }
}
