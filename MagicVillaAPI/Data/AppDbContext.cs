using MagicVillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // You can add DbSets later
        public DbSet<Villa> Villas { get; set; }
        // public DbSet<Villa> Villas { get; set; }
    }
}