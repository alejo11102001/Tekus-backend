using Tekus.Application.DTOs;
using Tekus.Domain.Entities;
using Tekus.Domain.Interfaces;

namespace Tekus.Application.Services;

public class ProviderAppService : IProviderAppService
{
    private readonly IProviderRepository _repo;
    public ProviderAppService(IProviderRepository repo) => _repo = repo;

    public async Task<PaginatedResult<ProviderDto>> ListAsync(int page, int pageSize, string q, string orderBy)
    {
        var (items, total) = await _repo.ListAsync(page, pageSize, q, orderBy);
        var dtos = items.Select(p => new ProviderDto {
            Id = p.Id, Nit = p.Nit, Name = p.Name, Email = p.Email, CreatedAt = p.CreatedAt, CustomFields = p.CustomFields
        }).ToList();
        return new PaginatedResult<ProviderDto> { Items = dtos, Total = total, Page = page, PageSize = pageSize };
    }

    public async Task<ProviderDto?> GetByIdAsync(int id)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p == null) return null;
        return new ProviderDto { Id = p.Id, Nit = p.Nit, Name = p.Name, Email = p.Email, CreatedAt = p.CreatedAt, CustomFields = p.CustomFields };
    }

    public async Task<ProviderDto> CreateAsync(CreateProviderDto dto)
    {
        var provider = new Provider(dto.Nit, dto.Name, dto.Email);
        await _repo.AddAsync(provider);
        return new ProviderDto { Id = provider.Id, Nit = provider.Nit, Name = provider.Name, Email = provider.Email, CreatedAt = provider.CreatedAt };
    }

    public async Task UpdateAsync(int id, CreateProviderDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) throw new KeyNotFoundException("Provider not found");
        existing.Update(dto.Name, dto.Email);
        await _repo.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}