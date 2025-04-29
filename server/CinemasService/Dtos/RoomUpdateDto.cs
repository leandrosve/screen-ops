using Common.Enums;

namespace CinemasService.Dtos
{

    public class RoomUpdateDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }

        public EntityStatus? Status { get; set; }
        public Guid? LayoutId { get; set; }
    }
}
