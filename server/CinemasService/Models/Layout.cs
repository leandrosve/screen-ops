using System.ComponentModel.DataAnnotations;

namespace CinemasService.Models
{
    public class Layout
    {
        public Guid Id { get; set; }

        [MaxLength(64)]
        public required string Name { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<LayoutElement> Elements { get; set; } = new List<LayoutElement>();
    }
}
