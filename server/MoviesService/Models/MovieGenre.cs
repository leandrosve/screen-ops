namespace MoviesService.Models
{
    public class MovieGenre
    {
        public int Id { get; set; }
        public Guid MovieId { get; set; }
        public required Movie Movie { get; set; }

        public int GenreId { get; set; }
        public required Genre Genre { get; set; }
    }
}
