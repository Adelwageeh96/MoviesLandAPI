using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Infrastructure.Data;
using Movies.Services.DTOs;
using Movies.Services.IServices;
using System.Linq.Expressions;

namespace Movies.Services.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MovieService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddMovieDetailsAsync(MovieDetailsDto dto)
        {
            var movie = _mapper.Map<Movie>(dto);

            try
            {
                // Ensure related entities exist
                foreach (var genreDto in dto.Genres)
                {
                    var genre = await EnsureEntityExistsAsync<Genre, GenreDto>(genreDto, g => g.Id == genreDto.Id);
                    if (!movie.MovieGenres.Any(mg => mg.GenreId == genre.Id))
                    {
                        movie.MovieGenres.Add(new MovieGenre { Movie = movie, Genre = genre });
                    }
                }

                foreach (var companyDto in dto.ProductionCompanies)
                {
                    var company = await EnsureEntityExistsAsync<ProductionCompany, ProductionCompanyDto>(companyDto, c => c.Id == companyDto.Id);
                    if (!movie.MovieProductionCompanies.Any(mpc => mpc.ProductionCompanyId == company.Id))
                    {
                        movie.MovieProductionCompanies.Add(new MovieProductionCompany { Movie = movie, ProductionCompany = company });
                    }
                }

                foreach (var countryDto in dto.ProductionCountries)
                {
                    var country = await EnsureEntityExistsAsync<ProductionCountry, ProductionCountryDto>(countryDto, c => c.Iso31661 == countryDto.Iso31661);
                    if (!movie.MovieProductionCountries.Any(mpc => mpc.Iso31661 == country.Iso31661))
                    {
                        movie.MovieProductionCountries.Add(new MovieProductionCountry { Movie = movie, ProductionCountry = country });
                    }
                }

                foreach (var languageDto in dto.SpokenLanguages)
                {
                    var language = await EnsureEntityExistsAsync<SpokenLanguage, SpokenLanguageDto>(languageDto, l => l.Iso6391 == languageDto.Iso6391);
                    if (!movie.MovieSpokenLanguages.Any(msl => msl.Iso6391 == language.Iso6391))
                    {
                        movie.MovieSpokenLanguages.Add(new MovieSpokenLanguage { Movie = movie, SpokenLanguage = language });
                    }
                }

                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public async Task<MovieDetailsDto?> GetMovieDetailsAsync(int movieId)
        {
            var movie = await _context.Movies
                            .Include(m => m.MovieGenres)
                                .ThenInclude(mg => mg.Genre)
                            .Include(m => m.MovieProductionCompanies)
                                .ThenInclude(pc => pc.ProductionCompany)
                            .Include(m => m.MovieProductionCountries)
                                .ThenInclude(pc => pc.ProductionCountry)
                            .Include(m => m.MovieSpokenLanguages)
                                .ThenInclude(sl => sl.SpokenLanguage)
                            .AsNoTracking()
                            .FirstOrDefaultAsync(m => m.Id == movieId);

            return movie is not null ? _mapper.Map<MovieDetailsDto>(movie) : null;

        }


        private async Task<TEntity> EnsureEntityExistsAsync<TEntity, TDto>(TDto dto, Expression<Func<TEntity, bool>> predicate)
         where TEntity : class
        {
            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).State = EntityState.Detached;
            }

            var trackedEntity = _context.ChangeTracker.Entries<TEntity>().FirstOrDefault(e => predicate.Compile().Invoke(e.Entity));
            if (trackedEntity != null)
            {
                return trackedEntity.Entity;
            }

            var entity = existingEntity ?? _mapper.Map<TEntity>(dto);
            if (existingEntity == null)
            {
                _context.Set<TEntity>().Add(entity);
            }
            return entity;
        }




    }
}
