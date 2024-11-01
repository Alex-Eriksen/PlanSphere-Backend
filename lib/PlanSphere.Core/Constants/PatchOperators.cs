namespace PlanSphere.Core.Constants;

public class PatchOperators
{
	/// <summary>
	/// Add a property or array element.
	/// <br/>
	/// For existing property: set value.
	/// </summary>
	public const string ADD = "add";

	/// <summary>
	/// Remove a property or array element.
	/// <br/>
	/// This will set a field to null.
	/// </summary>
	public const string REMOVE = "remove";

	/// <summary>
	/// Same as <c>remove</c> followed by add at same location.
	/// </summary>
	public const string REPLACE = "replace";

	/// <summary>
	/// Same as <c>remove</c> from source followed by <c>add</c> to destination using value from source.
	/// </summary>
	public const string MOVE = "move";

	/// <summary>
	/// Same as <c>add</c> to destination using value from source.
	/// </summary>
	public const string COPY = "copy";

	/// <summary>
	/// Return success status code if value at <c>path</c> = provided <c>value</c>.
	/// </summary>
	public const string TEST = "test";

	/// <summary>
	/// An array of default operations to make it easier to allow operators.
	/// </summary>
	public static readonly string[] DEFAULTS = [ADD, REMOVE, REPLACE];
}
