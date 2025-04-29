using AutoMapper;
using CinemasService.Dtos;
using CinemasService.Errors;
using CinemasService.Models;
using CinemasService.Repositories;
using CinemasService.Services.Interfaces;
using Common.Services;
using ScreenOps.Common;

namespace CinemasService.Services
{
    public class CinemaService : BaseService, ICinemaService
    {
        private readonly IMapper _mapper;
        private readonly ICinemaRepository _repository;

        public CinemaService(IMapper mapper, ICinemaRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ApiResult<CinemaDto>> Create(CinemaCreateDto dto, Guid userId)
        {
            var exists = await _repository.GetByName(dto.Name.Trim());
            if (exists!= null && !exists.IsDeleted)
            {
                return Fail<CinemaDto>(CinemaErrors.Create.NameAlreadyExists);
            }

            Cinema cinema = new()
            {
                Name = dto.Name.Trim(),
                Description = dto.Description,
                Location = dto.Location,
                Capacity = dto.Capacity,
                CreatedAt = DateTime.UtcNow,
                ImageUrl = dto.ImageUrl,
                CreatedBy = userId
            };

            await _repository.Insert(cinema);
            return Ok(_mapper.Map<CinemaDto>(cinema));
        }

        public async Task<ApiResult<bool>> Delete(Guid id, Guid userId)
        {
            var cinema = await _repository.GetById(id, true);
           
            if (cinema == null)
                return Fail<bool>(CinemaErrors.Delete.CinemaNotFound);
            
            cinema.DeletedBy = userId;
            cinema.DeletedAt = DateTime.UtcNow;

            await _repository.SaveChanges();
            return Ok(true);
        }

        public async Task<ApiResult<IEnumerable<CinemaSummaryDto>>> GetAll(bool includeDeleted, bool includeUnpublished)
        {
            var cinemas = await _repository.GetAll(includeDeleted, includeUnpublished);
            var dtos = _mapper.Map<IEnumerable<CinemaSummaryDto>>(cinemas);
            return Ok(dtos);
        }

        public async Task<ApiResult<CinemaDto>> GetById(Guid id, bool includeUnpublished)
        {
            var cinema = await _repository.GetById(id, includeUnpublished);

            if (cinema == null)
                return Fail<CinemaDto>(CinemaErrors.Get.CinemaNotFound);

            return Ok(_mapper.Map<CinemaDto>(cinema));
        }

        public async Task<ApiResult<CinemaDto>> Update(Guid id, CinemaUpdateDto dto)
        {
            var cinema = await _repository.GetById(id, true);
            if (cinema == null)
                return Fail<CinemaDto>(CinemaErrors.Update.CinemaNotFound);

            if (dto.Name != null)
                cinema.Name = dto.Name;

            if (dto.Location != null)
                cinema.Location = dto.Location;

            if (dto.Description != null)
                cinema.Description = dto.Description;

            if (dto.Capacity.HasValue)
                cinema.Capacity = dto.Capacity.Value;

            if (dto.Status.HasValue)
                cinema.Status = dto.Status.Value;

            if(dto.ImageUrl != null)
                cinema.ImageUrl = dto.ImageUrl;

            await _repository.SaveChanges();

            return Ok(_mapper.Map<CinemaDto>(cinema));
        }
    }
}
