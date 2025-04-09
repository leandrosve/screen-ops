using CinemasService.Enums;

namespace CinemasService.Dtos
{
    public class LayoutElementCreateDto
    {
        public string Label { get; set; } = "";
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public LayoutElementType Type { get; set; }
    }
}
