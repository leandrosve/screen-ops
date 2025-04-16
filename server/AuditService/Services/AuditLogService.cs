using AuditService.Dtos;
using AuditService.Errors;
using AuditService.Models;
using AuditService.Repositories;
using AutoMapper;
using Common.Models;
using Common.Services;
using ScreenOps.Common;

namespace AuditService.Services
{
    public class AuditLogService : BaseService , IAuditLogService
    {

        private readonly IAuditLogRepository _repository;
        private readonly IMapper _mapper;

        public AuditLogService(IAuditLogRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Create(AuditLogCreateDto dto)
        {
            AuditLog auditLog = _mapper.Map<AuditLog>(dto);
            await _repository.Create(auditLog);
            return true;
        }

        public async Task<ApiResult<PagedResult<AuditLogDto>>> GetByFilters(AuditLogSearchFiltersDto filters)
        {
            var res = await _repository.GetByFilters(filters);

            var dtos = _mapper.Map<PagedResult<AuditLogDto>>(res);

            return Ok(dtos);
        }

        public async Task<ApiResult<AuditLogDto>> GetById(int guid)
        {
            var res = await _repository.GetById(guid);

            if (res == null)
            {
                return Fail<AuditLogDto>(AuditLogErrors.Get.AuditLogNotFound);
            }
            return Ok(_mapper.Map<AuditLogDto>(res));
        }
    }
}
