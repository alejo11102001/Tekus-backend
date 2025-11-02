using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tekus.Infrastructure.Data;

namespace Tekus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly AppDbContext _db;
    public ReportsController(AppDbContext db) => _db = db;

    [HttpGet("summary")]
    public async Task<IActionResult> Summary()
    {
        // Services per country
        var servicesByCountry = await _db.ServiceCountries
            .Join(_db.Countries, sc => sc.CountryId, c => c.Id, (sc, c) => new { sc.ServiceId, CountryName = c.Name })
            .GroupBy(x => x.CountryName)
            .Select(g => new { Country = g.Key, ServicesCount = g.Select(x => x.ServiceId).Distinct().Count() })
            .ToListAsync();

        // Providers per country (providers that have at least one service in the country)
        var providersByCountry = await _db.ServiceCountries
            .Join(_db.Services, sc => sc.ServiceId, s => s.Id, (sc, s) => new { sc.CountryId, s.ProviderId })
            .Join(_db.Countries, x => x.CountryId, c => c.Id, (x, c) => new { c.Name, x.ProviderId })
            .GroupBy(x => x.Name)
            .Select(g => new { Country = g.Key, ProvidersCount = g.Select(x => x.ProviderId).Distinct().Count() })
            .ToListAsync();

        return Ok(new { servicesByCountry, providersByCountry });
    }
}