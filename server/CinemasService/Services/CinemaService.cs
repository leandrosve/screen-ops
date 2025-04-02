using AutoMapper;
using CinemasService.Dtos;
using CinemasService.Models;
using CinemasService.Repositories;
using ScreenOps.Common;

namespace CinemasService.Services
{
    public class CinemaService : ICinemaService
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
            Cinema cinema = new Cinema
            {
                Name = dto.Name,
                Description = dto.Description,
                Location = dto.Location,
                Capacity = dto.Capacity,
                CreatedAt = new DateTime(),
                CreatedBy = userId,
            };

            await _repository.Insert(cinema);
            return ApiResult<CinemaDto>.Ok(_mapper.Map<CinemaDto>(cinema));
        }

        public async Task<ApiResult<bool>> Delete(Guid id, Guid userId)
        {
            var cinema = await _repository.GetById(id);
           
            if (cinema == null)
                return ApiResult<bool>.Fail("cinema_not_found");
            
            cinema.DeletedBy = userId;
            cinema.DeletedAt = new DateTime();

            await _repository.SaveChanges();
            return ApiResult<bool>.Ok(true);
        }

        public async Task<ApiResult<IEnumerable<CinemaDto>>> GetAll(bool includeDeleted)
        {
            IEnumerable<Cinema> cinemas = await _repository.GetAll(includeDeleted);
            IEnumerable<CinemaDto> dtos = _mapper.Map<IEnumerable<CinemaDto>>(cinemas);
            return ApiResult<IEnumerable<CinemaDto>>.Ok(dtos);
        }

        public async Task<ApiResult<CinemaDto>> GetById(Guid id)
        {
            var cinema = await _repository.GetById(id);

            if (cinema == null)
                return ApiResult<CinemaDto>.Fail("cinema_not_found");

            return ApiResult<CinemaDto>.Ok(_mapper.Map<CinemaDto>(cinema));
        }

        public async Task<ApiResult<CinemaDto>> Update(Guid id, CinemaUpdateDto dto)
        {
            var cinema = await _repository.GetById(id);
            if (cinema == null)
                return ApiResult<CinemaDto>.Fail("cinema_not_found");

            if (dto.Name is not null)
                cinema.Name = dto.Name;

            if (dto.Location is not null)
                cinema.Location = dto.Location;

            if (dto.Description is not null)
                cinema.Description = dto.Description;

            if (dto.Capacity.HasValue)
                cinema.Capacity = dto.Capacity.Value;

            await _repository.SaveChanges();

            return ApiResult<CinemaDto>.Ok(_mapper.Map<CinemaDto>(cinema));
        }
    }
}
