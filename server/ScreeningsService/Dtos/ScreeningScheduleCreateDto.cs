using ScreeningsService.Enums;

namespace ScreeningsService.Dtos
{
    public class ScreeningScheduleCreateDto
    {
        public required Guid MovieId { get; set; }
        public required Guid RoomId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public ICollection<DayOfWeek> DaysOfWeek { get; set; } = new List<DayOfWeek>();
        public ICollection<ScreeningTimeDto> Times { get; set; } = new List<ScreeningTimeDto>();
        public ICollection<ScreeningFeatureEnum> Features { get; set; } = new List<ScreeningFeatureEnum>();
    }

}
