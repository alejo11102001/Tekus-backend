using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;

namespace Tekus.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Provider> Providers { get; set; } = null!;
    public DbSet<Service> Services { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
}