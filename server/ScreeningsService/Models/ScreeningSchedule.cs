using ScreeningsService.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScreeningsService.Models
{
    public class ScreeningSchedule
    {
        public Guid Id { get; set; }

        // External References
        public required Guid MovieId { get; set; }
        public required Guid RoomId { get; set; }
        public required Guid CinemaId { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public ICollection<ScreeningScheduleTime> Times { get; set; } = new List<ScreeningScheduleTime>();

        public ScreeningStatusEnum Status { get; set; } = ScreeningStatusEnum.Draft;

        public List<Screening> Screenings { get; set; } = new();

        [NotMapped]
        public List<ScreeningFeatureEnum> Features { get; set; } = new();

        [MaxLength(32)]
        public string FeaturesRaw
        {
            get => string.Join(",", Features.Select(f => (int) f));
            set => Features = string.IsNullOrEmpty(value)
                ? new List<ScreeningFeatureEnum>()
                : value.Split(',').Select(v => (ScreeningFeatureEnum)Int32.Parse(v)).ToList();
        }

        [NotMapped]
        public ICollection<DayOfWeek> DaysOfWeek { get; set; } = new List<DayOfWeek>();

        [MaxLength(14)]
        public string DaysOfWeekRaw
        {
            get => string.Join(",", DaysOfWeek.Select(d => ((int)d).ToString()));
            set => DaysOfWeek = string.IsNullOrWhiteSpace(value)
                ? new List<DayOfWeek>()
                : value.Split(',').Select(s => (DayOfWeek)int.Parse(s)).ToList();
        }
    }
}
