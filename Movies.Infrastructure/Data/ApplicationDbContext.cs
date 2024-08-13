using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions) { }


        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<ProductionCompany> ProductionCompanies { get; set; }
        public DbSet<ProductionCountry> ProductionCountries { get; set; }
        public DbSet<SpokenLanguage> SpokenLanguages { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieProductionCompany> MovieProductionCompanies { get; set; }
        public DbSet<MovieProductionCountry> MovieProductionCountries { get; set; }
        public DbSet<MovieSpokenLanguage> MovieSpokenLanguages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);

            modelBuilder.Entity<MovieGenre>()
                .HasOne(mg => mg.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);

            modelBuilder.Entity<MovieProductionCompany>()
                .HasKey(mpc => new { mpc.MovieId, mpc.ProductionCompanyId });

            modelBuilder.Entity<MovieProductionCompany>()
                .HasOne(mpc => mpc.Movie)
                .WithMany(m => m.MovieProductionCompanies)
                .HasForeignKey(mpc => mpc.MovieId);

            modelBuilder.Entity<MovieProductionCompany>()
                .HasOne(mpc => mpc.ProductionCompany)
                .WithMany(pc => pc.MovieProductionCompanies)
                .HasForeignKey(mpc => mpc.ProductionCompanyId);


            modelBuilder.Entity<MovieProductionCountry>()
                .HasOne(mpc => mpc.Movie)
                .WithMany(m => m.MovieProductionCountries)
                .HasForeignKey(mpc => mpc.MovieId);

            modelBuilder.Entity<MovieProductionCountry>()
                .HasOne(mpc => mpc.ProductionCountry)
                .WithMany(pc => pc.MovieProductionCountries)
                .HasForeignKey(mpc => mpc.Iso31661);

            modelBuilder.Entity<MovieSpokenLanguage>()
                .HasOne(msl => msl.Movie)
                .WithMany(m => m.MovieSpokenLanguages)
                .HasForeignKey(msl => msl.MovieId);

            modelBuilder.Entity<MovieSpokenLanguage>()
                .HasOne(msl => msl.SpokenLanguage)
                .WithMany(sl => sl.MovieSpokenLanguages)
                .HasForeignKey(msl => msl.Iso6391);
        }

    }
}
