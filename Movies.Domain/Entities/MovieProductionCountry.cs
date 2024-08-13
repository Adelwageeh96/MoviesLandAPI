using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Domain.Entities
{
    public class MovieProductionCountry
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("ProductionCountry")]
        public string? Iso31661 { get; set; }
        public ProductionCountry ProductionCountry { get; set; }
    }

}
