using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch.Operations;
using PlanSphere.Core.Constants;

namespace PlanSphere.Core.Extensions.ValidationExtensions;

public static class JsonPatchRuleExtensions
{
	public static IRuleBuilderOptions<T, TProperty> NotNull<T, TProperty>(this IRuleBuilder<T,TProperty> ruleBuilder, params string[] pathSegments) where TProperty : Operation
	{
		return ruleBuilder.Must((command, operation) =>
		{
			return !operation.path.Match(pathSegments) || PrepareOperationOp(operation.op) != PatchOperators.REMOVE || operation.value != null;
		}).WithMessage((command, operation) => $"Field '{operation.path}' cannot be null!");
	}

	public static IRuleBuilderOptions<T, TProperty> AllowOperators<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, params string[] allowedOperators) where TProperty : Operation
	{
		return ruleBuilder.Must((command, operation) =>
		{
			return allowedOperators.Contains(PrepareOperationOp(operation.op));
		}).WithMessage($"Non-supported operation type! Accepted types: [{string.Join(", ", allowedOperators)}]");
	}

	public static IRuleBuilderOptions<T, TProperty> DenyOperators<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, params string[] deniedOperators) where TProperty : Operation
	{
		return ruleBuilder.Must((command, operation) =>
		{
			return !deniedOperators.Contains(PrepareOperationOp(operation.op));
		}).WithMessage($"Non-supported operation type! Denied types: [{string.Join(", ", deniedOperators)}]");
	}

	public static IRuleBuilderOptions<T, TProperty> ValueGreaterThanZero<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, params string[] pathSegments) where TProperty : Operation
	{
		return ruleBuilder.Must((command, operation) =>
		{
			if (!operation.path.Match(pathSegments))
			{
				return true;
			}

			if (operation.value == null)
			{
				return false;
			}

			if (decimal.TryParse(operation.value.ToString(), out decimal decimalValue))
			{
				return decimalValue > 0;
			}

			return false;
		}).WithMessage((command, operation) => $"The value of '{operation.path}' must be greater than zero!");
	}

	public static IRuleBuilderOptions<T, TProperty> ValueNonNegative<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, params string[] pathSegments) where TProperty : Operation
	{
		return ruleBuilder.Must((command, operation) =>
		{
			if (!operation.path.Match(pathSegments))
			{
				return true;
			}

			if (operation.value == null)
			{
				return true;
			}

			if (decimal.TryParse(operation.value.ToString(), out decimal decimalValue))
			{
				return decimalValue >= 0;
			}

			return false;
		}).WithMessage((command, operation) => $"The value of '{operation.path}' must be greater than zero!");
	}

	public static IRuleBuilderOptions<T, TProperty> InRange<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, decimal minValue, decimal maxValue, params string[] pathSegments) where TProperty : Operation
	{
		return ruleBuilder.Must((command, operation) =>
		{
			if (!operation.path.Match(pathSegments))
			{
				return true;
			}

			if (operation.value == null)
			{
				return true;
			}

			if (decimal.TryParse(operation.value.ToString(), out var decimalValue))
			{
				return decimalValue <= maxValue && decimalValue >= minValue;
			}

			return false;
		}).WithMessage((command, operation) => $"The field '{operation.path}' must be greater or equal to '{minValue}' and less or equal to '{maxValue}'");
	}

	public static IRuleBuilderOptions<T, TProperty> DenyPatch<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, params string[] pathSegments) where TProperty : Operation
	{
		return ruleBuilder.Must((command, operation) =>
		{
			return !operation.path.Match(pathSegments);
		}).WithMessage((command, operation) => $"The field '{operation.path}' cannot be patched!");
	}

	public static IRuleBuilderOptions<T, TProperty> DenyArrayObjectPatch<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, string path) where TProperty : Operation
	{
		return ruleBuilder.Must((command, operation) =>
		{
			var parsedOpPath = PrepareOperationPath(operation.path);
			if (!parsedOpPath.Contains(path, StringComparison.CurrentCultureIgnoreCase))
			{
				return true;
			}
			var pathArray = parsedOpPath.Split("/", StringSplitOptions.RemoveEmptyEntries);
			if (pathArray[0] != path.ToLower())
			{
				return true;
			}
			return pathArray.Length > 2;
		}).WithMessage($"The field '{path}' cannot be patched as a whole, you must patch a specific value on that object!");
	}

	public static IRuleBuilderOptions<T, TProperty> NotEmptyString<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, params string[] pathSegments) where TProperty : Operation
	{
		return ruleBuilder.Must((command, operation) =>
		{
			if (!operation.path.Match(pathSegments))
			{
				return true;
			}

			if (operation.value is string stringValue)
			{
				return !string.IsNullOrWhiteSpace(stringValue);
			}

			return false;
		}).WithMessage((command, operation) => $"The value of '{operation.path}' cannot be an empty string!");
	}

	public static IRuleBuilderOptions<T, TProperty> IsEmailAddress<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, bool isRequired, params string[] pathSegments) where TProperty : Operation
	{
		var regex = new Regex(RegularExpressions.EmailAddress);

		return ruleBuilder.Must((command, operation) =>
		{
			if(!isRequired && string.IsNullOrEmpty(operation.value.ToString()))
			{
				return true;
			}

			if (!operation.path.Match(pathSegments))
			{
				return true;
			}

			return regex.IsMatch(operation.value.ToString());
		}).WithMessage((command, operation) => $"Please enter a valid email for field: {operation.path}");
	}

	private static string PrepareOperationPath(string path)
	{
		var preparedPath = path.ToLower().Trim();

		if (!path.StartsWith('/'))
		{
			return string.Join("", "/", preparedPath);
		}

		return preparedPath;
	}

	private static string PrepareOperationOp(string operation)
	{
		return operation.ToLower().Trim();
	}

	private static bool Match(this string origin, params string[] pathSegments)
	{
		var operationPathArray = PrepareOperationPath(origin).Split('/', StringSplitOptions.RemoveEmptyEntries);

		if (operationPathArray.Length != pathSegments.Length)
		{
			return false;
		}

		for (int index = 0; index < pathSegments.Length; index++)
		{
			var pathSegment = pathSegments[index].ToLower().Trim();

			if (pathSegment == "*")
			{
				continue;
			}

			if (operationPathArray[index] != pathSegment)
			{
				return false;
			}
		}

		return true;
	}
}
