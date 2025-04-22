using AutoMapper;
using Common.Models;
using Contracts.Movies;
using Contracts.Rooms;
using MoviesService;
using MoviesService.Dtos;
using MoviesService.Enums;
using MoviesService.Models;
using MoviesService.Static;

namespace CinemasService.Profiles
{
    public class MovieProfile : Profile
    {

        public MovieProfile()
        {
            CreateMap(typeof(PagedResult<>), typeof(PagedResult<>));
            CreateMap<Genre, GenreDto>();
            CreateMap<MovieMediaDto, MovieMedia>();
            CreateMap<MovieMediaCreateDto, MovieMedia>();

            CreateMap<MovieMedia, MovieMediaDto>();

            CreateMap<MovieCreateDto, Movie>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore()); // Se asigna después en el servicio

            CreateMap<MovieUpdateDto, Movie>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null)); // Sobreescribir solo si no es null
            CreateMap<MovieGenre, GenreDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Genre.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Genre.Id));

            CreateMap<Movie, MovieDto>()
                .ForMember(dest => dest.OriginalLanguage, opt => opt.MapFrom(x => LanguageConstants.GetByCode(x.OriginalLanguageCode)))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(x => CountryConstants.GetByCode(x.CountryCode)))
                .ForMember(dest => dest.TrailerUrl, opt =>
                    opt.MapFrom(x => x.Media.Where(m => m.Type == MovieMediaType.TRAILER.ToString()).Select(m => m.Url).FirstOrDefault()))
                .ForMember(dest => dest.PosterUrl, opt =>
                    opt.MapFrom(x => x.Media.Where(m => m.Type == MovieMediaType.POSTER.ToString()).Select(m => m.Url).FirstOrDefault()))
                 .ForMember(dest => dest.ExtraImageUrls, opt =>
                    opt.MapFrom(x => x.Media.Where(m => m.Type == MovieMediaType.EXTRA_IMAGE.ToString()).Select(m => m.Url).ToList()));

            CreateMap<Movie, MovieSummaryDto>();
            CreateMap<MovieDto, MovieSummaryDto>();

            CreateMap<MovieSummaryDto, GrpcMovieSummaryModel>();
        }

    }
}
