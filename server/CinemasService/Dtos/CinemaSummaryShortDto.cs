namespace CinemasService.Dtos
{
    public class CinemaSummaryShortDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
