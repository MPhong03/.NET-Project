using DotNETProject.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DotNETProject.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
