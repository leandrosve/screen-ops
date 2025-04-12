namespace Common.Models
{
    public class AuditableModel
    {
        public required DateTime CreatedAt { get; set; }
        public required Guid CreatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

        public bool IsDeleted => DeletedAt.HasValue;
    }
}
