namespace Contracts.Rooms
{
    public class RoomSummaryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public Guid CinemaId { get; set; }
    }
}
