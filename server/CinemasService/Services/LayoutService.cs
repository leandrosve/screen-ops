using AutoMapper;
using CinemasService.Dtos;
using CinemasService.Repositories;
using Common.Services;
using ScreenOps.Common;
using CinemasService.Models;
using CinemasService.Services.Interfaces;
using CinemasService.Enums;
using CinemasService.Errors;

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
            var sameName = await _repository.GetByName(dto.Name);
            if (sameName != null)
            {
                return Fail<LayoutDto>(LayoutErrors.Create.NameAlreadyExists);
            }
            var (valid, error) = ValidateStructure(dto);
            if (!valid) return Fail<LayoutDto>(error);

            var layout = _mapper.Map<Layout>(dto);

            layout.Elements = layout.Elements.OrderBy(e => e.Index).ToList();

            var counts = this.SeatCountsByType(layout.Elements);

            layout.StandardSeats = counts[LayoutElementType.STANDARD];
            layout.AccesibleSeats = counts[LayoutElementType.ACCESIBLE];
            layout.DisabledSeats = counts[LayoutElementType.DISABLED];
            layout.VipSeats = counts[LayoutElementType.VIP];

            layout.CreatedAt = DateTime.UtcNow;

            await _repository.Insert(layout);

            return Ok(_mapper.Map<LayoutDto>(layout));
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var layout = await _repository.GetById(id, true);

            if (layout == null)
                return Fail<bool>(LayoutErrors.Delete.LayoutNotFound);

            layout.DeletedAt = DateTime.UtcNow;

            await _repository.SaveChanges();
            return Ok(true);
        }

        public async Task<ApiResult<ICollection<LayoutSummaryDto>>> GetByFilters(LayoutSearchFiltersDto filters)
        {
            var layouts = await _repository.GetByFilters(filters);
            return Ok(_mapper.Map<ICollection<LayoutSummaryDto>>(layouts));
        }

        public async Task<ApiResult<LayoutDto>> GetById(Guid id, bool includeDeleted)
        {
            var layout = await _repository.GetById(id, includeDeleted);
            if (layout == null)
            {
                return Fail<LayoutDto>(LayoutErrors.Get.LayoutNotFound);
            }

            return Ok(_mapper.Map<LayoutDto>(layout));
        }

        public async Task<ApiResult<LayoutDto>> Update(Guid id, LayoutUpdateDto dto)
        {
            var layout = await _repository.GetById(id, true);

            if (layout == null)
                return Fail<LayoutDto>(LayoutErrors.Update.LayoutNotFound);

            if (dto.Name != null)
            {
                var sameName = await _repository.GetByName(dto.Name);
                if (sameName != null && sameName.Id != id)
                {
                    return Fail<LayoutDto>(LayoutErrors.Create.NameAlreadyExists);
                }
            }

            if ((dto.Rows != null) || (dto.Columns != null) || (dto.Elements != null))
            {

                if ((dto.Elements == null))
                {
                    return Fail<LayoutDto>(LayoutErrors.Create.ElementsRequired);
                }

                var rows = dto.Rows.GetValueOrDefault(layout.Rows);
                var columns = dto.Columns.GetValueOrDefault(layout.Columns);

                var (valid, error) = ValidateStructure(new LayoutCreateDto
                {
                    Columns = columns,
                    Rows = rows,
                    Elements = dto.Elements
                });
                if (!valid) return Fail<LayoutDto>(error);
            }

            if (dto.Columns.HasValue)
            {
                layout.Columns = dto.Columns.Value;
            }

            if (dto.Rows.HasValue)
            {
                layout.Rows = dto.Rows.Value;
            }

            if (dto.Name != null)
            {
                layout.Name = dto.Name;
            }

            if (dto.Elements != null)
            {
                layout.Elements = _mapper.Map<ICollection<LayoutElement>>(dto.Elements);
            }

            layout.Elements = layout.Elements.OrderBy(e => e.Index).ToList();

            var counts = this.SeatCountsByType(layout.Elements);

            layout.StandardSeats = counts[LayoutElementType.STANDARD];
            layout.AccesibleSeats = counts[LayoutElementType.ACCESIBLE];
            layout.DisabledSeats = counts[LayoutElementType.DISABLED];
            layout.VipSeats = counts[LayoutElementType.VIP];

            await _repository.SaveChanges();

            return Ok(_mapper.Map<LayoutDto>(layout));
        }

        private (bool, string) ValidateStructure(LayoutCreateDto layout)
        {
            const int MIN_SIZE = 4;
            const int MAX_TOTAL = 1500;


            if (layout.Rows < MIN_SIZE || layout.Columns < MIN_SIZE)
                return (false, LayoutErrors.Create.DimensionsTooSmall);

            if ((layout.Rows * layout.Columns) > MAX_TOTAL || (layout.Elements.Count > MAX_TOTAL))
                return (false, LayoutErrors.Create.ElementsTooMany);

            if (layout.Elements == null || layout.Elements.Count == 0)
                return (false, LayoutErrors.Create.ElementsRequired);

            int totalItems = (layout.Rows) * (layout.Columns);

            if (layout.Elements.Count != totalItems)
                return (false, LayoutErrors.Create.MissingSeatPositions);

            int seatCount = 0;
            var seatTypes = new HashSet<LayoutElementType>
                {
                    LayoutElementType.STANDARD,
                    LayoutElementType.VIP,
                    LayoutElementType.ACCESIBLE
                };

            var labels = new HashSet<string>();

            foreach (var element in layout.Elements)
            {
                if (seatTypes.Contains(element.Type))
                {
                    seatCount++;
                    if (element.Label == null) return (false, LayoutErrors.Create.SeatLabelRequired);
                    if (labels.Contains(element.Label)) return (false, LayoutErrors.Create.SeatLabelDuplicated);
                }
            }

            if (seatCount < MIN_SIZE * MIN_SIZE)
                return (false, LayoutErrors.Create.NotEnoughSeats);

            var occupiedPositions = new HashSet<int>();
            foreach (var element in layout.Elements)
            {
                if (!occupiedPositions.Add(element.Index))
                    return (false, LayoutErrors.Create.DuplicatePositions);
            }

            return (true, "");
        }

        private IDictionary<LayoutElementType, int> SeatCountsByType(ICollection<LayoutElement> elements)
        {
            var counts = new Dictionary<LayoutElementType, int>()
            {
                [LayoutElementType.STANDARD] = 0,
                [LayoutElementType.VIP] = 0,
                [LayoutElementType.ACCESIBLE] = 0,
                [LayoutElementType.DISABLED] = 0,
                [LayoutElementType.AISLE] = 0,
                [LayoutElementType.BLANK] = 0
            };

            foreach (var item in elements)
            {
                counts[item.Type]++;
            }

            return counts;
        }
    }
}
