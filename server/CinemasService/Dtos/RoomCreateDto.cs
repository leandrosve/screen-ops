namespace CinemasService.Dtos
{

    public class RoomCreateDto
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public Guid CinemaId { get; set; }
        public Guid LayoutId { get; set; }

    }
}
