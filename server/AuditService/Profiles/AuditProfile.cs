using AuditService.Dtos;
using AuditService.Models;
using AutoMapper;
using Common.Models;

namespace AuditService.Profiles
{
    public class AuditProfile : Profile
    {

        public AuditProfile() {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
            CreateMap<AuditLogCreateDto, AuditLog>();
            CreateMap<AuditLog, AuditLogDto>();
        }
    }
}
