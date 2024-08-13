using Microsoft.Extensions.Configuration;
using Movies.Services.DTOs;
using Movies.Services.IServices;
using Newtonsoft.Json;
using System.Net;

namespace Movies.Services.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;

        public TmdbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["TmdbApiKey"];
        }

        public async Task<MovieSearchResultDto> SearchMoviesAsync(string query)
        {
            var url = $"https://api.themoviedb.org/3/search/movie?query={Uri.EscapeDataString(query)}&api_key={_apiKey}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieSearchResultDto>(content);
                return result;
            }

            throw new Exception("An error occurred while searching for movies.");
        }

        public async Task<MovieSearchResultDto> GetPopularMoviesAsync(int pageNumber)
        {
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            var url = $"https://api.themoviedb.org/3/movie/popular?page={pageNumber}&api_key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieSearchResultDto>(content);
                return result;
            }

            throw new Exception("An error occurred while fetching movies.");
        }

        public async Task<MovieDetailsDto> GetMovieDetailsAsync(int movieId)
        {
            var url = $"https://api.themoviedb.org/3/movie/{movieId}?api_key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieDetailsDto>(content);
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new HttpRequestException($"There is no movie with ID {movieId}", null, HttpStatusCode.NotFound);
            }

            throw new Exception("An error occurred while fetching details.");
        }


    }
}
