using Common.Enums;

namespace CinemasService.Dtos
{
    public class CinemaDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public string Description { get; set; } = "";
        public int Capacity { get; set; }
        public EntityStatus Status  { get; set; }
        public string ImageUrl { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
