namespace CinemasService.Dtos
{
    public class RoomSearchFiltersDto
    {
        public bool IncludeDeleted { get; set; } = false;
        public bool IncludeUnpublished { get; set; } = false;
        public Guid? CinemaId { get; set; }
    }
}
