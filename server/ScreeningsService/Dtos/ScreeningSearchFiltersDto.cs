using ScreeningsService.Enums;

namespace ScreeningsService.Dtos
{
    public class ScreeningSearchFiltersDto
    {
        public Guid? CinemaId { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? MovieId { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
        public ICollection<ScreeningStatusEnum>? Status { get; set; }
    }
}
