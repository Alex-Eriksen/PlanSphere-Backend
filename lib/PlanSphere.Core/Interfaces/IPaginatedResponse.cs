namespace PlanSphere.Core.Interfaces;

public interface IPaginatedResponse<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<T> Results { get; set; }

}