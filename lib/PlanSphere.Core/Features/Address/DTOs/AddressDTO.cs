using PlanSphere.Core.Abstract;

namespace PlanSphere.Core.Features.Address.DTOs;

public class AddressDTO : BaseDTO
{
    public string StreetName { get; set; }
    public string HouseNumber { get; set; }
    public string? Door { get; set; }
    public string? Floor { get; set; }
    public string? PostalCode { get; set; }
    public string? CountryId { get; set; }
}