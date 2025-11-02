using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Tekus.Application.Services;
using Tekus.Domain.Entities;
using Tekus.Infrastructure.Data;

namespace Tekus.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly IHttpClientFactory _http;
        private readonly AppDbContext _db;

        public CountryService(IHttpClientFactory http, AppDbContext db)
        {
            _http = http;
            _db = db;
        }

        public async Task<IEnumerable<(string IsoCode, string Name)>> GetAllAsync(bool refresh = false)
        {
            if (!refresh && await _db.Countries.AnyAsync())
            {
                var cached = await _db.Countries
                    .AsNoTracking()
                    .OrderBy(c => c.Name)
                    .Select(c => new { c.IsoCode, c.Name })
                    .ToListAsync();
                
                return cached.Select(x => (x.IsoCode, x.Name));
            }

            var client = _http.CreateClient();
            var res = await client.GetFromJsonAsync<List<JsonElement>>("https://restcountries.com/v3.1/all");

            var countries = res!.Select(c =>
                {
                    var iso = c.TryGetProperty("cca2", out var v1) ? v1.GetString() ?? "" : "";
                    var name = c.TryGetProperty("name", out var v2) && v2.TryGetProperty("common", out var v3) ? v3.GetString() ?? "" : "";
                    return (Iso: iso, Name: name); 
                })
                .Where(x => !string.IsNullOrWhiteSpace(x.Iso) && !string.IsNullOrWhiteSpace(x.Name))
                .Select(x => (x.Iso, x.Name))
                .ToList();


            // ✅ cache upsert seguro
            foreach (var ct in countries)
            {
                var exists = await _db.Countries.FirstOrDefaultAsync(c => c.IsoCode == ct.Iso);
                if (exists == null)
                    _db.Countries.Add(new Country(ct.Iso, ct.Name));
                else
                    exists.Name = ct.Name;
            }

            await _db.SaveChangesAsync();
            return countries;
        }
    }
}
