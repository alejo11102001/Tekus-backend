using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tekus.Domain.Entities;

public class ServiceCountry
{
    [Required]
    public int ServiceId { get; set; }

    [Required]
    public int CountryId { get; set; }
}