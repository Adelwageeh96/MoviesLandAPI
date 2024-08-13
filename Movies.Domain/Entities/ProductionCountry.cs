using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.Entities
{
    public class ProductionCountry
    {
        [Key]
        public string? Iso31661 { get; set; }
        public string? Name { get; set; }

        // Navigation property
        public ICollection<MovieProductionCountry> MovieProductionCountries { get; set; }
    }

}
