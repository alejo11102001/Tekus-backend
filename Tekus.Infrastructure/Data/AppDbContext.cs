using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;

namespace Tekus.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

    public DbSet<Provider> Providers { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<ServiceCountry> ServiceCountries { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Set precision for decimal
        modelBuilder.Entity<Service>().Property(s => s.HourlyRateUsd).HasPrecision(18, 2);

        // Composite PK for junction
        modelBuilder.Entity<ServiceCountry>().HasKey(sc => new { sc.ServiceId, sc.CountryId });

        // Map CustomFieldsJson column length if necessary (nvarchar(max) default)
        modelBuilder.Entity<Provider>().Property(p => p.CustomFieldsJson).HasColumnType("nvarchar(max)");
    }
}