﻿using ScreeningsService.Dtos;
using ScreeningsService.Models;

namespace ScreeningsService.Repositories
{
    public interface IScreeningRepository
    {

        Task<ICollection<Screening>> GetByFilters(ScreeningSearchFiltersDto filters);

        Task<Screening?> GetById(Guid id);

        Task<Screening> Insert(Screening screening);

        Task SaveChanges();
    }
}
