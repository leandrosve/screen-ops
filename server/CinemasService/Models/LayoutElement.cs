using CinemasService.Enums;
using System.ComponentModel.DataAnnotations;

namespace CinemasService.Models
{
    public class LayoutElement
    {
        public Guid Id { get; set; }
        public Guid LayoutId { get; set; }
        public required Layout Layout{ get; set; }
        public required int PositionX { get; set; }
        public required int PositionY { get; set; }

        [MaxLength(24)]
        public LayoutElementType Type { get; set; }

        [MaxLength(5)]
        public string Label { get; set; } = "";
    }
}
