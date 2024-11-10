using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Features.JobTitles.DTOs;

public class JobTitleDTO : BaseDTO
{
    public string Name { get; set; }
    public bool IsInheritanceActive { get; set; }
    public SourceLevel SourceLevel { get; set; }
}