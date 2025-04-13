using ScreeningsService.Enums;

namespace ScreeningsService.Dtos
{
    public class ScreeningCreateDto
    {
        public Guid MovieId { get; set; }
        public Guid RoomId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }

        public List<ScreeningFeatureEnum> Features { get; set; } = new();
    }
}
