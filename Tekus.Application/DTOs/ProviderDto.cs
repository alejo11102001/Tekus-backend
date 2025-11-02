namespace Tekus.Application.DTOs;

public class ProviderDto
{
    public int Id { get; set; }
    public string Nit { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public Dictionary<string,string>? CustomFields { get; set; }
}