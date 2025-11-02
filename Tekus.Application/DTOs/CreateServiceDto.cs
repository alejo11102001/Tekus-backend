namespace Tekus.Application.DTOs;

public class CreateServiceDto
{
    public int ProviderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal HourlyRateUsd { get; set; }
    public List<int> CountryIds { get; set; } = new();
}