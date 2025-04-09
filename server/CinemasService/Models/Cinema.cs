using Common.Models;
using System.ComponentModel.DataAnnotations;

namespace CinemasService.Models
{
    public class Cinema : AuditableModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(128)]
        public required string Name { get; set; }
        [MaxLength(128)]
        public required string Location { get; set; }
        [MaxLength(512)]
        public required string Description { get; set; }
        public required int Capacity { get; set; }
        public required bool IsPublished { get; set; } = false;
    }
}
