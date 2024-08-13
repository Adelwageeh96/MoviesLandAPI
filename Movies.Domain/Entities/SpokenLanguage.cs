using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Entities
{
    public class SpokenLanguage
    {
        [Key]
        public string? Iso6391 { get; set; }
        public string? EnglishName { get; set; }
        public string? Name { get; set; }

        public ICollection<MovieSpokenLanguage> MovieSpokenLanguages { get; set; }
    }

}
