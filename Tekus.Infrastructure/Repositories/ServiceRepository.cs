using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.Data;

namespace Tekus.Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly AppDbContext _db;
    public ServiceRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Service service, IEnumerable<int> countryIds)
    {
        _db.Services.Add(service);
        await _db.SaveChangesAsync();

        foreach (var cid in countryIds.Distinct())
            _db.ServiceCountries.Add(new ServiceCountry { ServiceId = service.Id, CountryId = cid });

        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var s = await _db.Services.FindAsync(id);
        if (s != null)
        {
            // Remove junctions
            var joins = _db.ServiceCountries.Where(sc => sc.ServiceId == id);
            _db.ServiceCountries.RemoveRange(joins);

            _db.Services.Remove(s);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<Service?> GetByIdAsync(int id)
    {
        return await _db.Services.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<(IEnumerable<Service> Items, int Total)> ListAsync(int page, int pageSize, string q, string orderBy)
    {
        var query = _db.Services.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var qLower = q.ToLower();
            query = query.Where(s => s.Name.ToLower().Contains(qLower));
        }

        var total = await query.CountAsync();
        query = orderBy?.ToLower() == "hourlyrate" ? query.OrderByDescending(s => s.HourlyRateUsd) : query.OrderBy(s => s.Name);

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }

    public async Task UpdateAsync(Service service, IEnumerable<int> countryIds)
    {
        _db.Services.Update(service);

        // replace countries
        var existing = _db.ServiceCountries.Where(sc => sc.ServiceId == service.Id);
        _db.ServiceCountries.RemoveRange(existing);

        foreach (var cid in countryIds.Distinct())
            _db.ServiceCountries.Add(new ServiceCountry { ServiceId = service.Id, CountryId = cid });

        await _db.SaveChangesAsync();
    }
}
