namespace CinemasService.Dtos
{
    public class LayoutCreateDto
    {
        public string Name { get; set; } = "";
        public ICollection<LayoutElementCreateDto> Elements { get; set; } = new List<LayoutElementCreateDto>();
    }
}
