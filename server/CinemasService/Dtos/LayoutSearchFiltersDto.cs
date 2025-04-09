namespace CinemasService.Dtos
{
    public class LayoutSearchFiltersDto
    {
        public bool IncludeDeleted { get; set; } = false;
        public string? Name { get; set; }
    }
}
