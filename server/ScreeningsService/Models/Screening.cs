using ScreeningsService.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ScreeningsService.Models
{
    public class Screening
    {
        public Guid Id { get; set; }

        // External References
        public required Guid MovieId { get; set; }
        public required Guid RoomId { get; set; }
        public required Guid CinemaId { get; set; }
        public required DateOnly Date { get; set; }
        public required TimeOnly StartTime { get; set; }
        public required TimeOnly EndTime { get; set; }
        public ScreeningStatusEnum Status { get; set; } = ScreeningStatusEnum.Draft;
        public ScreeningSchedule? ScreeningSchedule { get; set; }
        public Guid? ScreeningScheduleId { get; set; }

        [NotMapped]
        public List<ScreeningFeatureEnum> Features { get; set; } = new();

        [MaxLength(32)]
        public string FeaturesRaw
        {
            get => string.Join(",", Features.Select(f => (int)f));
            set => Features = string.IsNullOrEmpty(value)
                ? new List<ScreeningFeatureEnum>()
                : value.Split(',').Select(v => (ScreeningFeatureEnum)Int32.Parse(v)).ToList();
        }
    }
}
