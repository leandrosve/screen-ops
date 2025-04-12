using ScreeningsService.Enums;

namespace ScreeningsService.Models
{
    public class ScreeningFeature
    {
        public Guid Id { get; set; }
        public Guid ScreeningId { get; set; }
        public required ScreeningFeatureEnum Feature { get; set; }
        public required Screening Screening { get; set; } = null!;
    }
}
