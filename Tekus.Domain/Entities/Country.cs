namespace Tekus.Domain.Entities;

public class Country
{
    public int Id { get; private set; }
    public string IsoCode { get; private set; }
    public string Name { get; private set; }

    protected Country() {}
    public Country(string isoCode, string name)
    {
        IsoCode = isoCode;
        Name = name;
    }
}