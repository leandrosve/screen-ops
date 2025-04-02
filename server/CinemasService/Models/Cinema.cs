using Common.Models;

namespace CinemasService.Models
{
    public class Cinema : AuditableModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public required string Location { get; set; }
        public required string Description { get; set; }
        public required int Capacity { get; set; }

    }
}
