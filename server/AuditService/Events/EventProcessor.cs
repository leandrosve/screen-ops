using AuditService.Dtos;
using AuditService.Models;
using AuditService.Services;
using AutoMapper;
using System.Text.Json;

namespace AuditService.Events
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<EventProcessor> _logger;

        public EventProcessor(IServiceScopeFactory scopeFactory, ILogger<EventProcessor> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        public async Task ProcessEvent(string message)
        {
            AuditLogCreateDto? auditLog;
            try
            {
                auditLog = JsonSerializer.Deserialize<AuditLogCreateDto>(message);
            }
            catch
            {
                _logger.LogWarning("[EventProcessor] --> Could not deserialize event");
                return;
            }

            if (auditLog == null)
            {
                _logger.LogWarning("[EventProcessor] --> Received empty message");
                return;
            }

            using (var scope = _scopeFactory.CreateScope())
            {

                var auditLogService = scope.ServiceProvider.GetRequiredService<IAuditLogService>();

                await auditLogService.Create(auditLog);
            }
        }
    }
}
