namespace Contracts.Movies
{
    public class MovieSummaryDto
    {
        public Guid Id {  get; set; }
        public string OriginalTitle { get; set; } = "";
        public string LocalizedTitle { get; set; } = "";
        public int Duration {  get; set; }
    }
}
