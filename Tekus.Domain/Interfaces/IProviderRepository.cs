using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces;

public interface IProviderRepository
{
    Task<Provider?> GetByIdAsync(int id);
    Task AddAsync(Provider provider);
    Task UpdateAsync(Provider provider);
    Task DeleteAsync(int id);
    Task<(IEnumerable<Provider> Items, int Total)> ListAsync(int page, int pageSize, string q, string orderBy);
}