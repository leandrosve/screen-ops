using Common.Dtos;
using MoviesService.Enums;

namespace MoviesService.Dtos
{
    public class MovieFiltersDto
    {
        public string? SearchTerm { get; set; }

        public bool IncludeDeleted { get; set; } = false;

        public ICollection<int>? Status { get; set; }

        public required PaginationFilterDto Pagination { get; set; }
    }
}
