namespace CinemasService.Dtos
{
    public class CinemaCreateDto
    {
        public string Name { get; set; } = "";
        public string Location { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        public int Capacity { get; set; }

    }
}
