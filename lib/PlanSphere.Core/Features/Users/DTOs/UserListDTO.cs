using PlanSphere.Core.Abstract;
using PlanSphere.Core.Common.DTOs;

namespace PlanSphere.Core.Features.Users.DTOs;

public class UserListDTO : BaseDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}