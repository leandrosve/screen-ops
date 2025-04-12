using ScreeningsService.Enums;

namespace ScreeningsService.Dtos
{
    public class ScreeningCreateDto
    {
        public Guid MovieId { get; set; }
        public Guid RoomId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public List<ScreeningFeatureEnum> Features { get; set; } = new();
    }
}
