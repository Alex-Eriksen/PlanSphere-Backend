namespace PlanSphere.Core.Interfaces;

public interface ISortableQuery<T> where T : Enum
{
	public T SortBy { get; init; }
	public bool SortDescending { get; init; }
}