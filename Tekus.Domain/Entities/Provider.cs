namespace Tekus.Domain.Entities;

public class Provider
{
    public int Id { get; private set; }
    public string Nit { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public List<Service> Services { get; private set; } = new();
    public Dictionary<string,string> CustomFields { get; private set; } = new();

    protected Provider() { } // para EF

    public Provider(string nit, string name, string email)
    {
        if (string.IsNullOrWhiteSpace(nit)) throw new ArgumentException("NIT required");
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email required");

        Nit = nit;
        Name = name;
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email required");
        Name = name;
        Email = email;
    }

    public void AddCustomField(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("Key required");
        CustomFields[key] = value;
    }
}