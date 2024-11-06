using PlanSphere.Core.Features.Address.Requests;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Departments.Request;

public class DepartmentUpdateRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Building { get; set; }
    public AddressRequest Address { get; set; }
}