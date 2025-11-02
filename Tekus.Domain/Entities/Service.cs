using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tekus.Domain.Entities;

public class Service
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    [Required]
    public int ProviderId { get; private set; }

    [Required]
    public string Name { get; private set; } = string.Empty;

    [Required]
    public decimal HourlyRateUsd { get; private set; }

    protected Service() { }

    public Service(int providerId, string name, decimal hourlyRateUsd)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        if (hourlyRateUsd < 0) throw new ArgumentException("HourlyRateUsd >= 0 required");
        ProviderId = providerId;
        Name = name;
        HourlyRateUsd = hourlyRateUsd;
    }

    public void Update(string name, decimal hourlyRateUsd)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        if (hourlyRateUsd < 0) throw new ArgumentException("HourlyRateUsd >= 0 required");
        Name = name;
        HourlyRateUsd = hourlyRateUsd;
    }
}