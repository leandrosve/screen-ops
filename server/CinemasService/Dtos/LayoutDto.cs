namespace CinemasService.Dtos
{
    public class LayoutDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ICollection<LayoutElementDto> Elements { get; set; } = new List<LayoutElementDto>();
    }
}
