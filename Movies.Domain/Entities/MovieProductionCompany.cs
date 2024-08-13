namespace Movies.Domain.Entities
{
    public class MovieProductionCompany
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int ProductionCompanyId { get; set; }
        public ProductionCompany ProductionCompany { get; set; }
    }

}
