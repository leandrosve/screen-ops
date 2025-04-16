using Common.Dtos;

namespace AuditService.Dtos
{
    public class AuditLogSearchFiltersDto
    {
        public Guid? UserId { get; set; }
        public string? Action { get; set; }
        public string? EntityType { get; set; }
        public Guid? EntityGuid { get; set; }
        public int? EntityId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? IpAddress { get; set; }
        public PaginationFilterDto Pagination { get; set; } = new (1, 20);
    }
}
