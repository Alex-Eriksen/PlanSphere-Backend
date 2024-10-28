using PlanSphere.Core.Abstract;

namespace PlanSphere.Core.Features.JobTitles.Requests;

public class JobTitleUpdateRequest
{
    public ulong Name { get; set; }
    public bool IsInheritanceActive { get; set; }
}