namespace Contracts.Rooms
{
    public class RoomSummaryContractDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public Guid CinemaId { get; set; }
    }
}
