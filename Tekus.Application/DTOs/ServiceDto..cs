namespace Tekus.Application.DTOs;

public class ServiceDto
{
    public int Id { get; set; }
    public int ProviderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal HourlyRateUsd { get; set; }
}