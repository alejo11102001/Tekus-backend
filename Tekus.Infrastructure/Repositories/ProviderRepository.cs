using Microsoft.EntityFrameworkCore;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;
using Tekus.Infrastructure.Data;

namespace Tekus.Infrastructure.Repositories;

public class ProviderRepository : IProviderRepository
{
    private readonly AppDbContext _db;
    public ProviderRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Provider provider)
    {
        _db.Providers.Add(provider);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var p = await _db.Providers.FindAsync(id);
        if (p != null)
        {
            _db.Providers.Remove(p);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<Provider?> GetByIdAsync(int id)
    {
        return await _db.Providers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<(IEnumerable<Provider> Items, int Total)> ListAsync(int page, int pageSize, string q, string orderBy)
    {
        var query = _db.Providers.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var qLower = q.ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(qLower) || p.Nit.ToLower().Contains(qLower) || p.Email.ToLower().Contains(qLower));
        }

        var total = await query.CountAsync();
        query = orderBy?.ToLower() == "createdat" ? query.OrderByDescending(p => p.CreatedAt) : query.OrderBy(p => p.Name);

        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }

    public async Task UpdateAsync(Provider provider)
    {
        _db.Providers.Update(provider);
        await _db.SaveChangesAsync();
    }
}