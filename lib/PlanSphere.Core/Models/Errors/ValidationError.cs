namespace PlanSphere.Core.Models.Errors;

public class ValidationError
{
    public string Field { get; set; }
    public List<string> Errors { get; set; }
}
