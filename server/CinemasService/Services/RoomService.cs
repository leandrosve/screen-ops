using AutoMapper;
using CinemasService.Dtos;
using CinemasService.Repositories;
using Common.Services;
using ScreenOps.Common;
using CinemasService.Models;
using CinemasService.Services.Interfaces;
using CinemasService.Errors;
using Contracts.Rooms;
using Common.Enums;

namespace CinemasService.Services
{
    public class RoomService : BaseService, IRoomService
    {
        private readonly IRoomRepository _repository;
        private readonly ICinemaRepository _cinemaRepository;

        private readonly IMapper _mapper;

        public RoomService(IRoomRepository repository, ICinemaRepository cinemaRepository, IMapper mapper)
        {
            _repository = repository;
            _cinemaRepository = cinemaRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<RoomDto>> Create(RoomCreateDto dto)
        {
            var cinema = await _cinemaRepository.GetById(dto.CinemaId, true);

            if (cinema == null)
            {
                return Fail<RoomDto>(RoomErrors.Create.CinemaNotFound);
            }

            Room room = new Room
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                Cinema = cinema,
            };

            await _repository.Insert(room);

            return Ok(_mapper.Map<RoomDto>(room));
        }

        public async Task<ApiResult<RoomDto>> Update(Guid id, RoomUpdateDto dto)
        {
            var room = await _repository.GetById(id);

            if (room == null || room.Status == EntityStatus.Deleted)
            {
                return Fail<RoomDto>(RoomErrors.Update.RoomNotFound);
            }


            _mapper.Map(dto, room);

            if (room.Status == EntityStatus.Published && room.Layout == null)
            {
                return Fail<RoomDto>(RoomErrors.Publish.LayoutMissing);
            }

            await _repository.SaveChanges();

            return Ok(_mapper.Map<RoomDto>(room));
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var room = await _repository.GetById(id);

            if (room == null)
                return Fail<bool>(RoomErrors.Delete.RoomNotFound);

            // Chequear condiciones para ver si se puede eliminar o no

            room.Status = EntityStatus.Deleted;

            await _repository.SaveChanges();
            return Ok(true);
        }

        public async Task<ApiResult<IEnumerable<RoomDto>>> GetByFilters(RoomSearchFiltersDto filters)
        {
            var rooms = await _repository.GetByFilters(filters);
            var dtos = _mapper.Map<IEnumerable<RoomDto>>(rooms);
            return Ok(dtos);
        }

        public async Task<ApiResult<RoomDto>> GetById(Guid id)
        {
            var room = await _repository.GetById(id);

            if (room == null)
                return Fail<RoomDto>(RoomErrors.Get.RoomNotFound);

            return Ok(_mapper.Map<RoomDto>(room));
        }

        public async Task<ApiResult<RoomSummaryDto>> GetSummary(Guid id)
        {
            var room = await _repository.GetById(id);

            if (room == null)
                return Fail<RoomSummaryDto>(RoomErrors.Get.RoomNotFound);

            return Ok(_mapper.Map<RoomSummaryDto>(room));
        }

    }
}
