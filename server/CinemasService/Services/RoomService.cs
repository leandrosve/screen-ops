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
        private readonly ILayoutRepository _layoutRepository;


        private readonly IMapper _mapper;

        public RoomService(IRoomRepository repository, ICinemaRepository cinemaRepository, ILayoutRepository layoutRepository, IMapper mapper)
        {
            _repository = repository;
            _cinemaRepository = cinemaRepository;
            _layoutRepository = layoutRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<RoomDto>> Create(RoomCreateDto dto)
        {
            var cinema = await _cinemaRepository.GetById(dto.CinemaId, true);

            if (cinema == null)
            {
                return Fail<RoomDto>(RoomErrors.Create.CinemaNotFound);
            }

            var layout = await _layoutRepository.GetById(dto.LayoutId, false);

            if (layout == null)
            {
                return Fail<RoomDto>(RoomErrors.Create.LayoutNotFound);
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

            if (dto.Name != null)
            {
                room.Name = dto.Name;
            }

            if (dto.Description != null)
            {
                room.Description = dto.Description;
            }


            if (dto.Status != null)
            {
                room.Status = dto.Status.Value;
            }

            if (dto.LayoutId != null)
            {
                var layout = await _layoutRepository.GetById(dto.LayoutId.Value, false);

                if (layout == null)
                {
                    return Fail<RoomDto>(RoomErrors.Create.LayoutNotFound);
                }
                room.Layout = layout;
            }

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

        public async Task<ApiResult<IEnumerable<RoomSummaryDto>>> GetByFilters(RoomSearchFiltersDto filters)
        {
            var rooms = await _repository.GetByFilters(filters);
            var dtos = _mapper.Map<IEnumerable<RoomSummaryDto>>(rooms);
            return Ok(dtos);
        }

        public async Task<ApiResult<RoomDto>> GetById(Guid id)
        {
            var room = await _repository.GetById(id);

            if (room == null)
                return Fail<RoomDto>(RoomErrors.Get.RoomNotFound);

            return Ok(_mapper.Map<RoomDto>(room));
        }

        public async Task<ApiResult<RoomSummaryContractDto>> GetSummary(Guid id)
        {
            var room = await _repository.GetById(id);

            if (room == null)
                return Fail<RoomSummaryContractDto>(RoomErrors.Get.RoomNotFound);

            return Ok(_mapper.Map<RoomSummaryContractDto>(room));
        }

    }
}
