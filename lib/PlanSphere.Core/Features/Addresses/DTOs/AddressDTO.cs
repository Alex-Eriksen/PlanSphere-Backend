using PlanSphere.Core.Abstract;

namespace PlanSphere.Core.Features.Addresses.DTOs;

public class AddressDTO : BaseDTO
{
    public AddressDTO? Parent { get; set; }
    public string? StreetName { get; set; }
    public string? HouseNumber { get; set; }
    public string? Door { get; set; }
    public string? Floor { get; set; }
    public string? PostalCode { get; set; }
    public string? CountryId { get; set; }
}