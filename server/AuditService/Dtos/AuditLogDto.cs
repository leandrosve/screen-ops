namespace AuditService.Dtos
{
    public class AuditLogDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Action { get; set; } = "";
        public string? EntityType { get; set; }
        public Guid? EntityGuid { get; set; }
        public int? EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? IpAddress { get; set; }
        public string? AdditionalData { get; set; }
    }
}
