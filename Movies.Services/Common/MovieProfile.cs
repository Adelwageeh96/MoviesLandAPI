using AutoMapper;
using Movies.Domain.Entities;
using Movies.Services.DTOs;

namespace Movies.Services.Common
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {

            CreateMap<MovieDetailsDto, Movie>()
                .ForMember(dest => dest.Adult, opt => opt.MapFrom(src => src.Adult))
                .ForMember(dest => dest.BackdropPath, opt => opt.MapFrom(src => src.BackdropPath))
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
                .ForMember(dest => dest.Homepage, opt => opt.MapFrom(src => src.Homepage))
                .ForMember(dest => dest.ImdbId, opt => opt.MapFrom(src => src.ImdbId))
                .ForMember(dest => dest.OriginalLanguage, opt => opt.MapFrom(src => src.OriginalLanguage))
                .ForMember(dest => dest.OriginalTitle, opt => opt.MapFrom(src => src.OriginalTitle))
                .ForMember(dest => dest.Overview, opt => opt.MapFrom(src => src.Overview))
                .ForMember(dest => dest.Popularity, opt => opt.MapFrom(src => src.Popularity))
                .ForMember(dest => dest.PosterPath, opt => opt.MapFrom(src => src.PosterPath))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.Revenue, opt => opt.MapFrom(src => src.Revenue))
                .ForMember(dest => dest.Runtime, opt => opt.MapFrom(src => src.Runtime))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Tagline, opt => opt.MapFrom(src => src.Tagline))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Video))
                .ForMember(dest => dest.VoteAverage, opt => opt.MapFrom(src => src.VoteAverage))
                .ForMember(dest => dest.VoteCount, opt => opt.MapFrom(src => src.VoteCount))
                .ForMember(dest => dest.MovieGenres, opt => opt.MapFrom(src => src.Genres.Select(g => new MovieGenre { GenreId = g.Id })))
                .ForMember(dest => dest.MovieProductionCompanies, opt => opt.MapFrom(src => src.ProductionCompanies.Select(pc => new MovieProductionCompany { ProductionCompanyId = pc.Id })))
                .ForMember(dest => dest.MovieProductionCountries, opt => opt.MapFrom(src => src.ProductionCountries.Select(pc => new MovieProductionCountry { Iso31661 = pc.Iso31661 })))
                .ForMember(dest => dest.MovieSpokenLanguages, opt => opt.MapFrom(src => src.SpokenLanguages.Select(sl => new MovieSpokenLanguage { Iso6391 = sl.Iso6391 })));

            CreateMap<Movie, MovieDetailsDto>()
                .ForMember(dest => dest.Adult, opt => opt.MapFrom(src => src.Adult ?? false))
                .ForMember(dest => dest.BackdropPath, opt => opt.MapFrom(src => src.BackdropPath))
                .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget ?? 0))
                .ForMember(dest => dest.Homepage, opt => opt.MapFrom(src => src.Homepage))
                .ForMember(dest => dest.ImdbId, opt => opt.MapFrom(src => src.ImdbId))
                .ForMember(dest => dest.OriginalLanguage, opt => opt.MapFrom(src => src.OriginalLanguage))
                .ForMember(dest => dest.OriginalTitle, opt => opt.MapFrom(src => src.OriginalTitle))
                .ForMember(dest => dest.Overview, opt => opt.MapFrom(src => src.Overview))
                .ForMember(dest => dest.Popularity, opt => opt.MapFrom(src => src.Popularity ?? 0))
                .ForMember(dest => dest.PosterPath, opt => opt.MapFrom(src => src.PosterPath))
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.Revenue, opt => opt.MapFrom(src => src.Revenue ?? 0))
                .ForMember(dest => dest.Runtime, opt => opt.MapFrom(src => src.Runtime ?? 0))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Tagline, opt => opt.MapFrom(src => src.Tagline))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Video, opt => opt.MapFrom(src => src.Video ?? false))
                .ForMember(dest => dest.VoteAverage, opt => opt.MapFrom(src => src.VoteAverage ?? 0))
                .ForMember(dest => dest.VoteCount, opt => opt.MapFrom(src => src.VoteCount ?? 0))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.MovieGenres.Select(mg => new GenreDto { Id = mg.GenreId, Name = mg.Genre.Name })))
                .ForMember(dest => dest.ProductionCompanies, opt => opt.MapFrom(src => src.MovieProductionCompanies.Select(mpc => new ProductionCompanyDto { Id = mpc.ProductionCompanyId, Name = mpc.ProductionCompany.Name, LogoPath = mpc.ProductionCompany.LogoPath, OriginCountry = mpc.ProductionCompany.OriginCountry })))
                .ForMember(dest => dest.ProductionCountries, opt => opt.MapFrom(src => src.MovieProductionCountries.Select(mpc => new ProductionCountryDto { Iso31661 = mpc.ProductionCountry.Iso31661, Name = mpc.ProductionCountry.Name })))
                .ForMember(dest => dest.SpokenLanguages, opt => opt.MapFrom(src => src.MovieSpokenLanguages.Select(msl => new SpokenLanguageDto { Iso6391 = msl.SpokenLanguage.Iso6391, Name = msl.SpokenLanguage.Name, EnglishName = msl.SpokenLanguage.EnglishName })));

            CreateMap<GenreDto, Genre>().ReverseMap();
            CreateMap<ProductionCompanyDto, ProductionCompany>().ReverseMap();
            CreateMap<ProductionCountryDto, ProductionCountry>().ReverseMap();
            CreateMap<SpokenLanguageDto, SpokenLanguage>().ReverseMap();
        }
    }
}
