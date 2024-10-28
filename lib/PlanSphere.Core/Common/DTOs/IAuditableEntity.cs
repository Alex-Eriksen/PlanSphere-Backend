namespace PlanSphere.Core.Common.DTOs;

public interface IAuditableEntity
{
    public interface IAuditableEntityDTO
    {
        public DateTime CreatedAt { get; set; }
        public ulong? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set;}
        public ulong? UpdatedBy { get; set; }
    }
}