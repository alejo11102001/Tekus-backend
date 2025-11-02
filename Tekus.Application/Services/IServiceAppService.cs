using Tekus.Application.DTOs;

namespace Tekus.Application.Services;

public interface IServiceAppService
{
    Task<PaginatedResult<ServiceDto>> ListAsync(int page, int pageSize, string q, string orderBy);
    Task<ServiceDto?> GetByIdAsync(int id);
    Task<ServiceDto> CreateAsync(CreateServiceDto dto);
    Task UpdateAsync(int id, CreateServiceDto dto);
    Task DeleteAsync(int id);
}