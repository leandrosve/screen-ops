using AutoMapper;
using CinemasService.Dtos;
using CinemasService.Repositories;
using Common.Services;
using ScreenOps.Common;
using CinemasService.Models;
using CinemasService.Services.Interfaces;
using CinemasService.Enums;

namespace CinemasService.Services
{
    public class LayoutService : BaseService, ILayoutService
    {
        private readonly ILayoutRepository _repository;
        private readonly IMapper _mapper;

        public LayoutService(ILayoutRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResult<LayoutDto>> Create(LayoutCreateDto dto)
        {
            var (valid, error) = ValidateStructure(dto);
            if (!valid) return Fail<LayoutDto>(error);

            var layout = _mapper.Map<Layout>(dto);

            layout.Elements = layout.Elements.OrderBy(e => e.PositionX)
                                .ThenBy(e => e.PositionY).ToList();
            
            layout.CreatedAt = DateTime.UtcNow;

            await _repository.Insert(layout);

            return Ok(_mapper.Map<LayoutDto>(layout));
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var layout = await _repository.GetById(id, true);

            if (layout == null)
                return Fail<bool>("layout_not_found");

            layout.DeletedAt = new DateTime();

            await _repository.SaveChanges();
            return Ok(true);
        }

        public async Task<ApiResult<ICollection<LayoutDto>>> GetByFilters(LayoutSearchFiltersDto filters)
        {
            var layouts = await _repository.GetByFilters(filters);
            return Ok(_mapper.Map<ICollection<LayoutDto>>(layouts));
        }

        public Task<ApiResult<LayoutDto>> GetById(Guid id, bool includeDeleted)
        {
            throw new NotImplementedException();
        }

        private (bool, string) ValidateStructure(LayoutCreateDto layout)
        {
            const int MIN_SIZE = 4;

            if (layout == null)
                return (false, "layout_required");

            if (layout.Elements == null || layout.Elements.Count == 0)
                return (false, "layout_elements_required");

            int dimensionX = 0, dimensionY = 0, seatCount = 0;
            var seatTypes = new HashSet<LayoutElementType>
                {
                    LayoutElementType.STANDARD,
                    LayoutElementType.VIP,
                    LayoutElementType.ACCESIBLE
                };

            var labels = new HashSet<string>();

            foreach (var element in layout.Elements)
            {
                dimensionX = Math.Max(dimensionX, element.PositionX);
                dimensionY = Math.Max(dimensionY, element.PositionY);

                if (seatTypes.Contains(element.Type))
                {
                    seatCount++;
                    if (element.Label == null) return (false, "layout_seat_label_required");
                    if (labels.Contains(element.Label)) return (false, "layout_seat_duplicated_label");
                }
            }

            if (dimensionX < MIN_SIZE || dimensionY < MIN_SIZE)
                return (false, "layout_dimensions_too_small");

            if (seatCount < MIN_SIZE * MIN_SIZE)
                return (false, "layout_not_enough_seats");

            var occupiedPositions = new HashSet<(int X, int Y)>();
            foreach (var element in layout.Elements)
            {
                var pos = (element.PositionX, element.PositionY);
                if (!occupiedPositions.Add(pos))
                    return (false, "layout_has_duplicated_positions");
            }

            int expectedTotal = (dimensionX + 1) * (dimensionY + 1);
            if (occupiedPositions.Count != expectedTotal)
                return (false, "layout_missing_positions");

            return (true, "");
        }
    }
}
