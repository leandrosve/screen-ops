namespace CinemasService.Dtos
{
    public class RoomSearchFiltersDto
    {
        public Guid? CinemaId { get; set; }
        public ICollection<int>? Status { get; set; }
    }
}
