namespace PlanSphere.Core.Features.Addresses.Requests;

public class AddressRequest
{
    public ulong? ParrentId { get; set; }
    public string? CountryId { get; set; }
    public string? StreetName { get; set; }
    public string? HouseNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? Door { get; set; }
    public string? Floor { get; set; }
}