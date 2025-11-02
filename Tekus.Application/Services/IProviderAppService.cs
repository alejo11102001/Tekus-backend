using Tekus.Application.DTOs;

namespace Tekus.Application.Services;

public interface IProviderAppService
{
    Task<PaginatedResult<ProviderDto>> ListAsync(int page, int pageSize, string q, string orderBy);
    Task<ProviderDto?> GetByIdAsync(int id);
    Task<ProviderDto> CreateAsync(CreateProviderDto dto);
    Task UpdateAsync(int id, CreateProviderDto dto);
    Task DeleteAsync(int id);
    Task AddCustomFieldAsync(int providerId, string key, string value);
}