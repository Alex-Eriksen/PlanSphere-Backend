using PlanSphere.Core.Features.Address.Requests;

namespace PlanSphere.Core.Features.Users.Requests;

public class UserRequest
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AddressRequest Address { get; set; }
}