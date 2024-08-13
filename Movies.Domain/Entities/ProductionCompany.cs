using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Domain.Entities
{
    public class ProductionCompany
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? LogoPath { get; set; }
        public string? Name { get; set; }
        public string? OriginCountry { get; set; }

        // Navigation property
        public ICollection<MovieProductionCompany> MovieProductionCompanies { get; set; }
    }

}
