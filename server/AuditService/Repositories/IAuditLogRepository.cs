using AuditService.Dtos;
using AuditService.Models;
using Common.Models;

namespace AuditService.Repositories
{
    public interface IAuditLogRepository
    {
        Task<AuditLog> Create(AuditLog auditLog);

        Task<AuditLog?> GetById(int id);

        Task<PagedResult<AuditLog>> GetByFilters(AuditLogSearchFiltersDto filters);
    }
}
