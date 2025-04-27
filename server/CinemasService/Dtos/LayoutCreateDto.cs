namespace CinemasService.Dtos
{
    public class LayoutCreateDto
    {
        public string Name { get; set; } = "";
        public int Rows { get; set; }
        public int Columns { get; set; }
        public ICollection<LayoutElementCreateDto> Elements { get; set; } = new List<LayoutElementCreateDto>();
    }
}
