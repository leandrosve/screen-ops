using Common.Enums;

namespace CinemasService.Dtos
{
    public class CinemaUpdateDto
    {
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public int? Capacity { get; set; }
        public string? ImageUrl { get; set; }
        public EntityStatus? Status { get; set; }

    }
}
