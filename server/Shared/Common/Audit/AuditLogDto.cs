using System.Net;

namespace Common.Audit
{
    public class AuditLogDto
    {
        public Guid UserId { get; set; }
        public string Action { get; set; } = "";
        public string? EntityType { get; set; }
        public Guid? EntityGuid { get; set; }
        public int? EntityId { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? IpAddress { get; set; }
        public string? AdditionalData { get; set; }
    }
}
