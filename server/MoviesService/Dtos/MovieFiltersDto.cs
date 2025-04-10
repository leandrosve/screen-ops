using Common.Dtos;

namespace MoviesService.Dtos
{
    public class MovieFiltersDto
    {
        public string? SearchTerm { get; set; }

        public bool IncludeDeleted { get; set; } = false;

        public required PaginationFilterDto Pagination { get; set; }
    }
}
