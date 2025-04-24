using AutoMapper;
using Common.Audit;
using Common.Enums;
using Common.Models;
using Common.Services;
using Contracts.Movies;
using MoviesService.Dtos;
using MoviesService.Enums;
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

        private readonly IAuditClient _auditClient;


        private readonly IMapper _mapper;

        public MovieService(IMovieRepository movieRepository, IGenreRepository genreRepository, IMapper mapper, IAuditClient auditClient)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
            _auditClient = auditClient;
        }

        public async Task<ApiResult<MovieDto>> Create(MovieCreateDto dto)
        {
            if (!dto.ForceCreate)
            {
                var movieWithSameTitle = await _movieRepository.GetByExactTitleAndYearAsync(dto.OriginalTitle, dto.OriginalReleaseYear);

                if (movieWithSameTitle != null)
                {
                    return Fail<MovieDto>(MovieErrors.Create.OriginalTitleRepeated);
                }
            }

            Movie movie = _mapper.Map<Movie>(dto);


            movie.Status = EntityStatus.Draft;
            movie.CreatedAt = DateTime.UtcNow;

            var genres = await _genreRepository.GetByIds(dto.GenreIds);

            movie.Genres = genres.Select(genre => new MovieGenre { Genre = genre, Movie = movie }).ToList();

            movie.Media = BuildMovieMedia(movie, dto);

            await _movieRepository.Insert(movie);

            var movieDto = _mapper.Map<MovieDto>(movie);

            return Ok(movieDto);
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

            if ((dto.OriginalTitle != null || dto.OriginalReleaseYear != null) && !dto.ForceUpdate)
            {
                var title = dto.OriginalTitle ?? movie.OriginalTitle;
                var year = dto.OriginalReleaseYear ?? movie.OriginalReleaseYear;
                var movieWithSameTitle = await _movieRepository.GetByExactTitleAndYearAsync(title, year);

                if (movieWithSameTitle != null)
                {
                    return Fail<MovieDto>(MovieErrors.Update.OriginalTitleRepeated);
                }
            }

            _mapper.Map(dto, movie);


            if (dto.GenreIds != null)
            {
                var genres = await _genreRepository.GetByIds(dto.GenreIds);

                movie.Genres = genres.Select(genre => new MovieGenre { Genre = genre, Movie = movie }).ToList();
            }

            UpdateMovieMedia(movie, dto);

            await _movieRepository.SaveChanges();

            var resultDto = _mapper.Map<MovieDto>(movie);
            return Ok(resultDto);
        }

        private ICollection<MovieMedia> BuildMovieMedia(Movie movie, MovieCreateDto dto)
        {
            var media = new List<MovieMedia>();

            media.Add(new MovieMedia { Movie = movie, Type = MovieMediaType.TRAILER.ToString(), Url = dto.TrailerUrl });
            media.Add(new MovieMedia { Movie = movie, Type = MovieMediaType.POSTER.ToString(), Url = dto.PosterUrl });

            foreach (var url in dto.ExtraImageUrls)
            {
                media.Add(new MovieMedia { Movie = movie, Type = MovieMediaType.EXTRA_IMAGE.ToString(), Url = url });
            }

            return media;
        }

        private void UpdateMovieMedia(Movie movie, MovieUpdateDto dto)
        {
            if (dto.TrailerUrl != null)
            {
                var trailer = movie.Media.FirstOrDefault(m => m.Type == MovieMediaType.TRAILER.ToString());

                if (trailer is not null)
                    trailer.Url = dto.TrailerUrl;
                else
                    movie.Media.Add(new MovieMedia
                    {
                        Movie = movie,
                        Type = MovieMediaType.TRAILER.ToString(),
                        Url = dto.TrailerUrl
                    });
            }

            if (dto.PosterUrl != null)
            {
                var trailer = movie.Media.FirstOrDefault(m => m.Type == MovieMediaType.POSTER.ToString());

                if (trailer is not null)
                    trailer.Url = dto.PosterUrl;
                else
                    movie.Media.Add(new MovieMedia
                    {
                        Movie = movie,
                        Type = MovieMediaType.POSTER.ToString(),
                        Url = dto.PosterUrl
                    });
            }

            if (dto.ExtraImageUrls != null)
            {
                var media = movie.Media.Where(m => m.Type != MovieMediaType.EXTRA_IMAGE.ToString()).ToList();
                foreach (var item in dto.ExtraImageUrls)
                {
                    media.Add(new MovieMedia { Movie = movie, Type = MovieMediaType.EXTRA_IMAGE.ToString(), Url = item });
                }
                movie.Media = media;
            }
        }
    }
}
