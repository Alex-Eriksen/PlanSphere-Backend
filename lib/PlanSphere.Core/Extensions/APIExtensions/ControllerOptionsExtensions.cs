using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlanSphere.Core.Models.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace PlanSphere.Core.Extensions.APIExtensions;

public static class ControllerOptionsExtensions
{
    public static MvcOptions AddSwaggerErrorResponses(this MvcOptions options)
    {
        options.Filters.Add(new SwaggerResponseAttribute(StatusCodes.Status400BadRequest, "An invalid or missing input parameter will result in a bad request", typeof(BaseErrorResponse)));
        options.Filters.Add(new SwaggerResponseAttribute(StatusCodes.Status401Unauthorized, "The user is not authorized to access this resource", typeof(BaseErrorResponse)));
        options.Filters.Add(new SwaggerResponseAttribute(StatusCodes.Status403Forbidden, "The user is not allowed access this resource", typeof(BaseErrorResponse)));
        options.Filters.Add(new SwaggerResponseAttribute(StatusCodes.Status404NotFound, "Entity not found", typeof(BaseErrorResponse)));
        options.Filters.Add(new SwaggerResponseAttribute(StatusCodes.Status500InternalServerError, "Internal server error, please contact support", typeof(BaseErrorResponse)));
        return options;
    }
}