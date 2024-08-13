using Movies.Services.DTOs;

namespace Movies.Services.IServices
{
    public interface ITmdbService
    {
        Task<MovieSearchResultDto> SearchMoviesAsync(string query);
        Task<MovieSearchResultDto> GetPopularMoviesAsync(int pageNumber);
        Task<MovieDetailsDto> GetMovieDetailsAsync(int movieId);

    }
}
