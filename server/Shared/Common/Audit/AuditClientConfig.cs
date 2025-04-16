namespace Common.Audit
{
    public class AuditClientConfig
    {
        public string HostName { get; set; } = "localhost";
        public int Port { get; set; } = 5672;
        public string QueueName { get; set; } = "audit-logs";
    }
}
