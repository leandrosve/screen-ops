namespace CinemasService.Dtos
{
    public class CinemaDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public string Description { get; set; } = "";
        public int Capacity { get; set; }

    }
}
