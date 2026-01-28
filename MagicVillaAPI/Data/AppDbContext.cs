using MagicVillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaAPI.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User -> Wallets (1:N)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Wallets)
            .WithOne(w => w.User)
            .HasForeignKey(w => w.UserId);

        // User -> Reservations (1:N)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Reservations)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId);

        // Villa -> Reservations (1:N)
        modelBuilder.Entity<Villa>()
            .HasMany(v => v.Reservations)
            .WithOne(r => r.Villa)
            .HasForeignKey(r => r.VillaId);

        modelBuilder.Entity<AuditRecord>();
    }
    public DbSet<Villa> Villas { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<AuditRecord> AuditRecords { get; set; }
    
}