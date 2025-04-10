namespace Common.Dtos
{
    public class PaginationFilterDto
    {
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 20;

        public PaginationFilterDto(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
