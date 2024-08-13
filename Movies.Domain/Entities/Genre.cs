using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.Domain.Entities
{
    public class Genre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? Name { get; set; }

        // Navigation property
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }

}
