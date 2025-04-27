using CinemasService.Enums;

namespace CinemasService.Dtos
{
    public class LayoutElementCreateDto
    {
        public string Label { get; set; } = "";
        public int Index { get; set; }
        public LayoutElementType Type { get; set; }
    }
}
