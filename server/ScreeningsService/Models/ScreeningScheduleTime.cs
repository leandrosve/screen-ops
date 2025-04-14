namespace ScreeningsService.Models
{
    public class ScreeningScheduleTime
    {
        public Guid Id { get; set; }
        public Guid ScheduleId { get; set; }

        public required ScreeningSchedule Schedule { get; set; }
        public required TimeOnly Start { get; set; }
        public required TimeOnly End { get; set; }
    }
}
