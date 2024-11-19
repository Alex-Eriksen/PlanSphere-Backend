using PlanSphere.Core.Features.Addresses.Requests;

namespace PlanSphere.Core.Features.Users.Requests;

public class UserPatchRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public AddressRequest Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateOnly? Birthday { get; set; }
    public List<ulong>? RoleIds { get; set; }
    public List<ulong>? JobTitleIds { get; set; }
    
    public UserSettingsPatchRequest Settings { get; set; }
}