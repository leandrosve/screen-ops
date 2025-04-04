namespace MoviesService.Models
{
    public class MovieMedia
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public required string Type { get; set; } // Poster, Trailer, etc.
        public required Movie Movie { get; set; }
    }
}
