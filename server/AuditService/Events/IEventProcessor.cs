namespace AuditService.Events
{
    public interface IEventProcessor
    {
        Task ProcessEvent(string message);
    }
}
