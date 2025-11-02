using Tekus.Domain.Entities;

namespace Tekus.Domain.Interfaces;

public interface IServiceRepository
{
    Task<Service?> GetByIdAsync(int id);
    Task AddAsync(Service service, IEnumerable<int> countryIds);
    Task UpdateAsync(Service service, IEnumerable<int> countryIds);
    Task DeleteAsync(int id);
    Task<(IEnumerable<Service> Items, int Total)> ListAsync(int page, int pageSize, string q, string orderBy);
}