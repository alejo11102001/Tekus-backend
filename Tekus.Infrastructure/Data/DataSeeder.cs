using Tekus.Domain.Entities;

namespace Tekus.Infrastructure.Data;

public class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (!db.Countries.Any())
        {
            db.Countries.AddRange(
                new Country("CO", "Colombia"),
                new Country("PE", "Peru"),
                new Country("MX", "Mexico"),
                new Country("US", "United States"),
                new Country("BR", "Brazil"),
                new Country("AR", "Argentina"),
                new Country("CL", "Chile"),
                new Country("EC", "Ecuador"),
                new Country("UY", "Uruguay"),
                new Country("PA", "Panama")
            );
            await db.SaveChangesAsync();
        }

        if (!db.Providers.Any())
        {
            for (int i = 1; i <= 10; i++)
            {
                var p = new Provider($"900{i:00000}", $"Provider {i}", $"provider{i}@example.com");
                db.Providers.Add(p);
            }

            await db.SaveChangesAsync();
        }
    }
}