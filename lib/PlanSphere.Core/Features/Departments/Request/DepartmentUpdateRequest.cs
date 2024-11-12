using PlanSphere.Core.Features.Addresses.Requests;

namespace PlanSphere.Core.Features.Departments.Request;

public class DepartmentUpdateRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Building { get; set; }
    public AddressRequest Address { get; set; }
    public bool? InheritDefaultWorkSchedule { get; set; }
}