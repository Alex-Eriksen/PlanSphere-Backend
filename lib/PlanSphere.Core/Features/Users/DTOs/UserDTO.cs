using PlanSphere.Core.Abstract;
using PlanSphere.Core.Common.DTOs;
using PlanSphere.Core.Features.Address.DTOs;

namespace PlanSphere.Core.Features.Users.DTOs;

public class UserDTO : BaseDTO, IAuditableEntityDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public AddressDTO Address { get; set; }
    public string? PhoneNumber { get; set; }
    public DateOnly? Birthday { get; set; }
    
    public UserSettingsDTO Settings { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
}