namespace Common.Audit
{
    public class AuthorInfo
    {
        public required Guid Id { get; set; }

        public required string IpAddress { get; set; }
    }
}
