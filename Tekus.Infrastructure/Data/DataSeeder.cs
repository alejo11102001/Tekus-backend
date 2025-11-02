using Tekus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tekus.Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        // Countries
        if (!await db.Countries.AnyAsync())
        {
            var countries = new[]
            {
                new Country("CO","Colombia"), new Country("MX","Mexico"), new Country("US","United States"),
                new Country("AR","Argentina"), new Country("CL","Chile"), new Country("BR","Brazil"),
                new Country("PE","Peru"), new Country("EC","Ecuador"), new Country("CA","Canada"), new Country("ES","Spain")
            };
            db.Countries.AddRange(countries);
            await db.SaveChangesAsync();
        }

        // Providers
        if (!await db.Providers.AnyAsync())
        {
            for (int i = 1; i <= 10; i++)
            {
                var p = new Provider($"900{i:00000}", $"Provider {i}", $"provider{i}@example.com");
                p.AddOrUpdateCustomField("notes", $"seeded provider #{i}");
                db.Providers.Add(p);
            }
            await db.SaveChangesAsync();
        }

        // Services
        if (!await db.Services.AnyAsync())
        {
            var svc1 = new Service(1, "Cloud Infra", 45.50m);
            var svc2 = new Service(1, "Consulting Azure", 60.00m);
            var svc3 = new Service(2, "AWS Support", 50.00m);
            db.Services.AddRange(svc1, svc2, svc3);
            await db.SaveChangesAsync();

            // Link services to countries (simple)
            db.ServiceCountries.Add(new ServiceCountry { ServiceId = svc1.Id, CountryId = 1 });
            db.ServiceCountries.Add(new ServiceCountry { ServiceId = svc1.Id, CountryId = 2 });
            db.ServiceCountries.Add(new ServiceCountry { ServiceId = svc2.Id, CountryId = 3 });
            await db.SaveChangesAsync();
        }
    }
}