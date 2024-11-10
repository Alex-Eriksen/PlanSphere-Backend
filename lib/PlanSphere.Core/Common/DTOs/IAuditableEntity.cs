namespace PlanSphere.Core.Common.DTOs;

public interface IAuditableEntityDTO
{
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set;}
    public string? UpdatedBy { get; set; }
}
