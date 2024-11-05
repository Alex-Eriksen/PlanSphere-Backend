using PlanSphere.Core.Abstract;

namespace PlanSphere.Core.Features.Rights.DTOs;

public class RightLookUpDTO : BaseLookUpDTO<ulong>
{
    public string Description { get; set; }
}