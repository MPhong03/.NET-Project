using DotNetProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DotNetProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
