namespace CinemasService.Dtos
{
    public class LayoutSummaryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int Rows { get; set; }
        public int Columns { get; set; }

        // Totales, para evitar calcular a menudo
        public int StandardSeats { get; set; } = 0;
        public int VipSeats { get; set; } = 0;
        public int AccesibleSeats { get; set; } = 0;
        public int DisabledSeats { get; set; } = 0;
    }
}
