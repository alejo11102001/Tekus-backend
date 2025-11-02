namespace Tekus.Application.Services;

public interface ICountryService
{
    Task<IEnumerable<(string IsoCode, string Name)>> GetAllAsync(bool refresh = false);
}