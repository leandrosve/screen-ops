using Common.Dtos;
using ScreeningsService.Enums;

namespace ScreeningsService.Dtos
{
    public class ScreeningSearchFiltersDto
    {
        public Guid? CinemaId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? MovieId { get; set; }

        public ICollection<ScreeningStatusEnum>? Status { get; set; }
        public ICollection<ScreeningFeatureEnum>? Features { get; set; }
    }
}
