using Movies.Services.DTOs;

namespace Movies.Services.IServices
{
    public interface IMovieService
    {
        Task<MovieDetailsDto?> GetMovieDetailsAsync(int movieId);
        Task AddMovieDetailsAsync(MovieDetailsDto dto);
    }
}
