using Common.Enums;
using MoviesService.Enums;

namespace MoviesService.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public required string OriginalTitle { get; set; }
        public required string LocalizedTitle { get; set; }
        public required string Description { get; set; }
        public required string Director { get; set; }
        public required string MainActors { get; set; }

        public required int Duration { get; set; }
        public required int OriginalReleaseYear { get; set; }
        public required string CountryCode { get; set; }
        public required string OriginalLanguageCode { get; set; }

        public required ICollection<MovieGenre> Genres{ get; set; } = new List<MovieGenre>();
        public required ICollection<MovieMedia> Media { get; set; } = new List<MovieMedia>();
        public required EntityStatus Status { get; set; } = EntityStatus.Draft;
        public required DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
