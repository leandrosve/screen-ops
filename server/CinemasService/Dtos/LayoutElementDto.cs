using CinemasService.Enums;

namespace CinemasService.Dtos
{
    public class LayoutElementDto
    {
        public Guid Id { get; set; }
        public string Label { get; set; } = "";
        public int Index { get; set; }
        public LayoutElementType Type { get; set; }
    }
}
