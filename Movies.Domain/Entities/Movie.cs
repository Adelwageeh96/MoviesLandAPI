using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Domain.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public bool? Adult { get; set; }
        public string? BackdropPath { get; set; }
        public int? Budget { get; set; }
        public string? Homepage { get; set; }
        public string? ImdbId { get; set; }
        public string? OriginalLanguage { get; set; }
        public string? OriginalTitle { get; set; }
        public string? Overview { get; set; }
        public double? Popularity { get; set; }
        public string? PosterPath { get; set; }
        public string? ReleaseDate { get; set; }
        public int? Revenue { get; set; }
        public int? Runtime { get; set; }
        public string? Status { get; set; }
        public string? Tagline { get; set; }
        public string? Title { get; set; }
        public bool? Video { get; set; }
        public double? VoteAverage { get; set; }
        public int? VoteCount { get; set; }

        // Navigation properties for many-to-many relationships
        public ICollection<MovieGenre> MovieGenres { get; set; }
        public ICollection<MovieProductionCompany> MovieProductionCompanies { get; set; }
        public ICollection<MovieProductionCountry> MovieProductionCountries { get; set; }
        public ICollection<MovieSpokenLanguage> MovieSpokenLanguages { get; set; }
    }

}
