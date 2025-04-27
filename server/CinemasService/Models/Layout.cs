using System.ComponentModel.DataAnnotations;

namespace CinemasService.Models
{
    public class Layout
    {
        public Guid Id { get; set; }

        [MaxLength(64)]
        public required string Name { get; set; }
        public required DateTime CreatedAt { get; set; }

        public required int Rows{ get; set; }
        public required int Columns { get; set; }

        public DateTime? DeletedAt { get; set; }
        public ICollection<LayoutElement> Elements { get; set; } = new List<LayoutElement>();

        // Totales, para evitar calcular a menudo
        public required int StandardSeats { get; set; }
        public required int VipSeats { get; set; }
        public required int AccesibleSeats { get; set; }
        public required int DisabledSeats { get; set; }
    }
}
