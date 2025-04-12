using ScreeningsService.Enums;

namespace ScreeningsService.Models
{
    public class Screening
    {
        public Guid Id { get; set; }

        // External References
        public required Guid MovieId { get; set; }
        public required Guid RoomId { get; set; }
        public required Guid CinemaId { get; set; }

        public required DateTime StartTime { get; set; }
        public required DateTime EndTime { get; set; }
        public ICollection<ScreeningFeature> Features { get; set; } = new List<ScreeningFeature>();
        public ScreeningStatusEnum Status { get; set; } = ScreeningStatusEnum.Draft;
    }
}
