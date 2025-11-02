namespace Tekus.Application.DTOs;

public class ProviderDto
{
    public int Id { get; set; }
    public string Nit { get; set; } = "";
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public Dictionary<string,string>? CustomFields { get; set; }
}