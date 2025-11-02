using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Tekus.Domain.Entities;

public class Provider
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [Required]
    public string Nit { get; private set; } = string.Empty;

    [Required]
    public string Name { get; private set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; private set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    // Column to persist dynamic custom fields as JSON
    public string? CustomFieldsJson { get; private set; }

    // Not mapped: helper to work with dictionary in memory
    [NotMapped]
    public Dictionary<string, string> CustomFields
    {
        get
        {
            if (string.IsNullOrEmpty(CustomFieldsJson)) return new();
            return JsonSerializer.Deserialize<Dictionary<string, string>>(CustomFieldsJson)!
                   ?? new Dictionary<string, string>();
        }
        private set => CustomFieldsJson = JsonSerializer.Serialize(value);
    }

    protected Provider() { } // EF

    public Provider(string nit, string name, string email)
    {
        if (string.IsNullOrWhiteSpace(nit)) throw new ArgumentException("NIT required");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email required");

        Nit = nit;
        Name = name;
        Email = email;
        CreatedAt = DateTime.UtcNow;
        CustomFields = new Dictionary<string, string>();
    }

    public void Update(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email required");
        Name = name;
        Email = email;
    }

    public void AddOrUpdateCustomField(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Key required");
        var dict = CustomFields;
        dict[key] = value;
        CustomFields = dict;
    }

    public void RemoveCustomField(string key)
    {
        var dict = CustomFields;
        if (dict.Remove(key)) CustomFields = dict;
    }
}
