using DotNETProject.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DotNETProject.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<TVSeries> TVSeries { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<FilmCast> FilmCasts { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<FilmDirector> FilmDirectors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Nation> Nations { get; set; }

    }
}
