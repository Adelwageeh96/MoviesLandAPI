using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Domain.Entities
{
    public class MovieSpokenLanguage
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("SpokenLanguage")]
        public string? Iso6391 { get; set; }
        public SpokenLanguage SpokenLanguage { get; set; }
    }

}
