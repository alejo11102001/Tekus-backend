using Tekus.Application.DTOs;
using Tekus.Domain.Interfaces;
using Tekus.Domain.Entities;

namespace Tekus.Application.Services;

public class ServiceAppService : IServiceAppService
{
    private readonly IServiceRepository _repo;
    public ServiceAppService(IServiceRepository repo) => _repo = repo;

    public async Task<PaginatedResult<ServiceDto>> ListAsync(int page, int pageSize, string q, string orderBy)
    {
        var (items, total) = await _repo.ListAsync(page, pageSize, q, orderBy);
        var dtos = items.Select(s => new ServiceDto { Id = s.Id, ProviderId = s.ProviderId, Name = s.Name, HourlyRateUsd = s.HourlyRateUsd }).ToList();
        return new PaginatedResult<ServiceDto> { Items = dtos, Total = total, Page = page, PageSize = pageSize };
    }

    public async Task<ServiceDto?> GetByIdAsync(int id)
    {
        var s = await _repo.GetByIdAsync(id);
        if (s == null) return null;
        return new ServiceDto { Id = s.Id, ProviderId = s.ProviderId, Name = s.Name, HourlyRateUsd = s.HourlyRateUsd };
    }

    public async Task<ServiceDto> CreateAsync(CreateServiceDto dto)
    {
        var svc = new Service(dto.ProviderId, dto.Name, dto.HourlyRateUsd);
        await _repo.AddAsync(svc, dto.CountryIds);
        return new ServiceDto { Id = svc.Id, ProviderId = svc.ProviderId, Name = svc.Name, HourlyRateUsd = svc.HourlyRateUsd };
    }

    public async Task UpdateAsync(int id, CreateServiceDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) throw new KeyNotFoundException("Service not found");
        existing.Update(dto.Name, dto.HourlyRateUsd);
        await _repo.UpdateAsync(existing, dto.CountryIds);
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}