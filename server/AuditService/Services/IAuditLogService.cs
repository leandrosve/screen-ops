using AuditService.Dtos;
using AuditService.Models;
using Common.Models;
using ScreenOps.Common;

namespace AuditService.Services
{
    public interface IAuditLogService
    {
        Task<bool> Create(AuditLogCreateDto auditLog);

        Task<ApiResult<AuditLogDto>> GetById(int guid);

        Task<ApiResult<PagedResult<AuditLogDto>>> GetByFilters(AuditLogSearchFiltersDto filters);
    }
}