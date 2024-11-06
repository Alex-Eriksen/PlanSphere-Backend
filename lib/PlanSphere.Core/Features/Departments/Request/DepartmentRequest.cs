using MediatR;
using PlanSphere.Core.Features.Address.Requests;

namespace PlanSphere.Core.Features.Departments.Request;

public class DepartmentRequest : IRequest 
{
    public string DepartmentName { get; set; }
    public string Description { get; set; }
    public string Building { get; set; }
    public AddressRequest Address { get; set; }
}