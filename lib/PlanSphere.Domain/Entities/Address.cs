using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class Address : BaseEntity
{
    public ulong? ParentId { get; set; }
    public Address? Parent { get; set; }
    
    public string? CountryId { get; set; }
    public Country? Country { get; set; }
    
    public string? StreetName { get; set; }
    public string? HouseNumber { get; set; }
    
    public string? PostalCode { get; set; }
    public ZipCode? ZipCode { get; set; }
    
    public string? Door { get; set; }
    public string? Floor { get; set; }
}