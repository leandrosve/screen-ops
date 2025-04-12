using AutoMapper;
using Common.Models;
using Common.Services;
using Contracts.Movies;
using MoviesService.Dtos;
using MoviesService.Errors;
using MoviesService.Models;
using MoviesService.Repositories;
using ScreenOps.Common;

namespace MoviesService.Services
{
    public class MovieService : BaseService, IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;

        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IGenreRepository genreRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<ApiResult<MovieDto>> Create(MovieCreateDto dto)
        {
            Movie movie = _mapper.Map<Movie>(dto);

            movie.CreatedAt = DateTime.UtcNow;

            var genres = await _genreRepository.GetByIds(dto.GenreIds);

            movie.Genres = genres.Select(genre => new MovieGenre { Genre = genre, Movie = movie }).ToList();

            await _movieRepository.Insert(movie);

            return Ok(_mapper.Map<MovieDto>(movie));
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            Movie? movie = await _movieRepository.GetById(id);
            if (movie == null)
            {
                return Fail<bool>(MovieErrors.Delete.MovieNotFound);
            }

            movie.DeletedAt = DateTime.UtcNow;

            var res = await _movieRepository.SaveChanges();
            return Ok(res);
        }

        public async Task<ApiResult<MovieDto>> Get(Guid id)
        {
            Movie? movie = await _movieRepository.GetById(id);
            if (movie == null) {
                return Fail<MovieDto>(MovieErrors.Get.MovieNotFound);
            }
            return Ok(_mapper.Map<MovieDto>(movie));
        }

        public async Task<ApiResult<MovieSummaryDto>> GetSummary(Guid id)
        {
            Movie? movie = await _movieRepository.GetById(id);
            if (movie == null)
            {
                return Fail<MovieSummaryDto>(MovieErrors.Get.MovieNotFound);
            }
            return Ok(_mapper.Map<MovieSummaryDto>(movie));
        }

        public async Task<ApiResult<PagedResult<MovieDto>>> GetByFilters(MovieFiltersDto filters)
        {
            var res = await _movieRepository.GetByFilters(filters);

            var dtos = _mapper.Map<PagedResult<MovieDto>>(res);

            return Ok(dtos);
        }

        public async Task<ApiResult<MovieDto>> Update(Guid id, MovieUpdateDto dto)
        {
            var movie = await _movieRepository.GetById(id);
            if (movie == null)
            {
                return Fail<MovieDto>(MovieErrors.Update.MovieNotFound);
            }

            _mapper.Map(dto, movie);


            if (dto.GenreIds != null)
            {
                var genres = await _genreRepository.GetByIds(dto.GenreIds);

                movie.Genres = genres.Select(genre => new MovieGenre { Genre = genre, Movie = movie }).ToList();
            }

            await _movieRepository.SaveChanges();

            var resultDto = _mapper.Map<MovieDto>(movie);
            return Ok(resultDto);
        }
    }
}
