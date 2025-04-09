using System.ComponentModel.DataAnnotations;

namespace CinemasService.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        [MaxLength(256)]
        public required string Name { get; set; }
        [MaxLength(256)]
        public required string Description { get; set; }
        public required Cinema Cinema { get; set; }
        public Guid CinemaId { get; set; }
        public Layout? Layout { get; set; }
        public int LayoutId { get; set; }
        public DateTime? PublishedAt { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
