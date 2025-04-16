using AuditService.Dtos;
using AuditService.Models;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using ScreenOps.CinemasService.Data;

namespace AuditService.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {

        private AppDBContext _context;

        public AuditLogRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<AuditLog> Create(AuditLog auditLog)
        {
            await _context.AuditLogs.AddAsync(auditLog);

            await _context.SaveChangesAsync();

            return auditLog;
        }

        public async Task<PagedResult<AuditLog>> GetByFilters(AuditLogSearchFiltersDto filters)
        {
            var query = _context.AuditLogs.AsQueryable();

            if (filters.UserId != null) query = query.Where(m => m.UserId == filters.UserId);
            if (filters.Action != null) query = query.Where(m => m.Action == filters.Action);
            if (filters.EntityType != null)query = query.Where(m => m.EntityType == filters.EntityType);
            if (filters.EntityGuid != null) query = query.Where(m => m.EntityGuid == filters.EntityGuid);
            if (filters.EntityId != null) query = query.Where(m => m.EntityId == filters.EntityId);

            if (filters.IpAddress != null) query = query.Where(m => m.IpAddress == filters.IpAddress);

            if (filters.FromDate != null || filters.ToDate != null)
            {
                var fromDate = filters.FromDate ?? DateTime.MinValue;
                var toDate = filters.ToDate ?? DateTime.MaxValue;

                query = query.Where(m =>
                   m.Timestamp >= fromDate &&
                   m.Timestamp <= toDate
                );
            }

            var totalCount = await query.CountAsync();
            var pagination = filters.Pagination;

            var offset = Math.Max(pagination.Page - 1, 0) * pagination.PageSize;
            query = query.Skip(offset).Take(pagination.PageSize);

            var res = await query.ToListAsync();

            return new PagedResult<AuditLog>
            {
                Items = res,
                PageNumber = pagination.Page,
                PageSize = pagination.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<AuditLog?> GetById(int id)
        {
            var query = _context.AuditLogs.AsQueryable();
            return await query.Where(u => u.Id == id).FirstOrDefaultAsync();
        }
    }
}
