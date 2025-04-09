namespace CinemasService.Dtos
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public LayoutDto Layout { get; set; } = null!;
        public DateTime? PublishedAt { get; set; }

    }
}
