using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class Country
{
    public string? IsoCode { get; set; }
    public string? Name { get; set; }
    public string? IsdCode { get; set; }
}