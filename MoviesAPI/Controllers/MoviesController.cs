using Microsoft.AspNetCore.Mvc;
using Movies.Services.DTOs;
using Movies.Services.IServices;
using System.Net;

namespace Movies.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ITmdbService _tmdbService;
        private readonly IMovieService _movieService;

        public MoviesController(ITmdbService tmdbService, IMovieService movieService)
        {
            _tmdbService = tmdbService;
            _movieService = movieService;
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            try
            {
                var result = await _tmdbService.SearchMoviesAsync(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { MessageError = ex.Message });
            }
        }

        [HttpGet("popular")]
        public async Task<IActionResult> Popular([FromQuery] int pageNumber)
        {
            try
            {
                var result = await _tmdbService.GetPopularMoviesAsync(pageNumber);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { MessageError = ex.Message });

            }
        }

        [HttpGet("Details/{movieId}")]
        public async Task<IActionResult> Details(int movieId)
        {
            try
            {
                if (await _movieService.GetMovieDetailsAsync(movieId) is MovieDetailsDto result)
                {
                    return Ok(result);
                }

                result = await _tmdbService.GetMovieDetailsAsync(movieId);

                await _movieService.AddMovieDetailsAsync(result);
                return Ok(result);
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound(new { MessageError = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { MessageError = ex.Message });

            }
        }

    }
}
