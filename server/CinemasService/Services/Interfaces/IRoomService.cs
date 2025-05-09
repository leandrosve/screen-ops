﻿using CinemasService.Dtos;
using Contracts.Rooms;
using ScreenOps.Common;

namespace CinemasService.Services.Interfaces
{
    public interface IRoomService
    {
        public Task<ApiResult<RoomDto>> Create(RoomCreateDto dto);

        public Task<ApiResult<RoomDto>> Update(Guid id, RoomUpdateDto dto);

        public Task<ApiResult<RoomDto>> GetById(Guid id);
        public Task<ApiResult<RoomSummaryContractDto>> GetSummary(Guid id);

        public Task<ApiResult<IEnumerable<RoomSummaryDto>>> GetByFilters(RoomSearchFiltersDto dto);

        public Task<ApiResult<bool>> Delete(Guid id);
    }
}
