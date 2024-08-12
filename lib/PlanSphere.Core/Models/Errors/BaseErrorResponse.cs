namespace PlanSphere.Core.Models.Errors;

public class BaseErrorResponse
{
    public string Message { get; set; }
    public IEnumerable<ValidationError> ValidationErrors { get; set; }
}