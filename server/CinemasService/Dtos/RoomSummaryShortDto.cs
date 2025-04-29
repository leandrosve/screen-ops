using Common.Enums;

namespace CinemasService.Dtos
{
    public class RoomSummaryShortDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime? PublishedAt { get; set; }
        public EntityStatus status { get; set; }
    }
}
