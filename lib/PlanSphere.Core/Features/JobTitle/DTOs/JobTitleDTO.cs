using PlanSphere.Core.Abstract;

namespace PlanSphere.Core.Features.JobTitle.DTOs;

public class JobTitleDTO : BaseDTO
{
    public string Name { get; set; }
    public bool IsInheritanceActive { get; set; }
}