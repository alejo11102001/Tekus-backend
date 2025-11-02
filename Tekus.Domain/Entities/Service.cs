namespace Tekus.Domain.Entities;

public class Service
{
    public int Id { get; private set; }
    public int ProviderId { get; private set; }
    public string Name { get; private set; }
    public decimal HourlyRateUsd { get; private set; }
    public List<Country> Countries { get; private set; } = new();

    protected Service() {}

    public Service(int providerId, string name, decimal hourlyRateUsd)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new System.ArgumentException("Name required");
        if (hourlyRateUsd < 0) throw new System.ArgumentException("HourlyRate >= 0 required");
        ProviderId = providerId;
        Name = name;
        HourlyRateUsd = hourlyRateUsd;
    }

    public void Update(string name, decimal hourlyRateUsd)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new System.ArgumentException("Name required");
        if (hourlyRateUsd < 0) throw new System.ArgumentException("HourlyRate >= 0 required");
        Name = name;
        HourlyRateUsd = hourlyRateUsd;
    }
}