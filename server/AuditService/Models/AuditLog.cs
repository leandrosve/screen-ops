using System.ComponentModel.DataAnnotations;

namespace AuditService.Models
{

    public class AuditLog
    {
        public int Id { get; set; }
        public required Guid UserId { get; set; }

        [MaxLength(100)]
        public required string Action { get; set; }

        [MaxLength(50)]
        public string? EntityType { get; set; }
        public Guid? EntityGuid { get; set; }
        public int? EntityId { get; set; }
        public required DateTime Timestamp { get; set; }

        [MaxLength(50)]
        public string? IpAddress { get; set; }
        public string? AdditionalData { get; set; }
    }
}
