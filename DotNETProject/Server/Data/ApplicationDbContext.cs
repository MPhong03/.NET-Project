using DotNETProject.Server.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
