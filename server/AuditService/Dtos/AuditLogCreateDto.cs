using System.ComponentModel.DataAnnotations;

namespace AuditService.Dtos
{
    public class AuditLogCreateDto
    {
        public required Guid UserId { get; set; }
        public required string Action { get; set; }
        public string? EntityType { get; set; }
        public Guid? EntityGuid { get; set; }
        public int? EntityId { get; set; }
        public required DateTime Timestamp { get; set; }
        public string? IpAddress { get; set; }
        public string? AdditionalData { get; set; }
    }
}
