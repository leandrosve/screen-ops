using ScreeningsService.Enums;
using ScreeningsService.Models;

namespace ScreeningsService.Dtos
{
    public class ScreeningScheduleDto
    {
        public Guid Id { get; set; }
        public required Guid MovieId { get; set; }
        public required Guid RoomId { get; set; }
        public required Guid CinemaId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<ScreeningFeatureEnum> Features { get; set; } = new();
        public ScreeningScheduleStatusEnum Status { get; set; } = ScreeningScheduleStatusEnum.Draft;
        public ICollection<ScreeningTimeDto> Times { get; set; } = new List<ScreeningTimeDto>();
        public ICollection<DayOfWeek> DaysOfWeek { get; set; } = new List<DayOfWeek>();
        public List<ScreeningDto> Screenings { get; set; } = new();

    }
}
