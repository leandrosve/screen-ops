namespace Common.Audit
{
    public interface IAuditClient
    {
        Task InitializeAsync();
        Task Log(AuditLogDto log);
    }
}
