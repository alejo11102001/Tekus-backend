namespace Tekus.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string IsoCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public Country(string isoCode, string name)
        {
            IsoCode = isoCode;
            Name = name;
        }

        // Constructor vacío para EF
        public Country() { }
    }
}