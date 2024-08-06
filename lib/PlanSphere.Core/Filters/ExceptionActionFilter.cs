using System.Net.Mime;
using System.Text;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Exceptions;
using PlanSphere.Core.Models.Errors;

namespace PlanSphere.Core.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ExceptionActionFilter : ExceptionFilterAttribute
{
	private readonly ILogger<ExceptionActionFilter> _logger;

	public ExceptionActionFilter(ILogger<ExceptionActionFilter> logger)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public override void OnException(ExceptionContext context)
	{
		HandleException(context);
		base.OnException(context);
	}

	public override Task OnExceptionAsync(ExceptionContext context)
	{
		HandleException(context);
		return base.OnExceptionAsync(context);
	}

	private void HandleException(ExceptionContext context)
	{
		if (context.ExceptionHandled)
		{
			return;
		}

		var statusCode = GetStatusCode(context.Exception);
		context.HttpContext.Response.StatusCode = statusCode;
		LogException(context.Exception, statusCode, context.HttpContext.Request.Path.Value);

		context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;

		var baseErrorResponse = GetBaseErrorResponse(context.Exception);
		var responseString = JsonSerializer.Serialize(baseErrorResponse);
		var responseBytes = Encoding.UTF8.GetBytes(responseString);

		context.HttpContext.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
		context.ExceptionHandled = true;
	}

	private BaseErrorResponse GetBaseErrorResponse(Exception exception)
	{
		var message = exception.Message;
		var validationErrors = new List<ValidationError>();

		if (exception is ValidationException validationException)
		{
			message = "Invalid Request";
			var errors = validationException.Errors;

			validationErrors = errors
				.GroupBy(x => x.PropertyName)
				.Select(x => new ValidationError()
				{
					Field = x.Key,
					Errors = x.Select(e => e.ErrorMessage).Distinct().ToList()
				})
				.ToList();
		}

		return new BaseErrorResponse() { Message = message, ValidationErrors = validationErrors };
	}

	private void LogException(Exception exception, int statusCode, string path)
	{
		var logMessage = "Exception: {exception} - status code: {statusCode} - in path: {path}";
		var loglevel = statusCode switch
		{
			>= StatusCodes.Status400BadRequest and <= StatusCodes.Status404NotFound => LogLevel.Information,
			>= StatusCodes.Status500InternalServerError => LogLevel.Error,
			_ => LogLevel.Warning,
		};

		_logger.Log(loglevel, logMessage, exception, statusCode, path);
	}

	private int GetStatusCode(Exception exception)
	{
		return exception switch
		{
			NotImplementedException => StatusCodes.Status501NotImplemented,
			UnauthorizedAccessException  => StatusCodes.Status401Unauthorized,
			ArgumentNullException
				or ArgumentException
				or NotSupportedException
				or ValidationException
				or NullReferenceException
				or KeyNotFoundException
				or FormatException => StatusCodes.Status400BadRequest,
			ForbiddenAccessException
				or ForbiddenActionException => StatusCodes.Status403Forbidden,
						_ => StatusCodes.Status500InternalServerError,
		};
	}
}