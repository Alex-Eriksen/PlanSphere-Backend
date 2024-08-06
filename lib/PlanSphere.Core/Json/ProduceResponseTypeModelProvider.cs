using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace PlanSphere.Core.Json;

public class ProduceResponseTypeModelProvider : IApplicationModelProvider
{
    public int Order => 1;

    public void OnProvidersExecuted(ApplicationModelProviderContext context)
    {
    }

    public void OnProvidersExecuting(ApplicationModelProviderContext context)
    {
        foreach (ControllerModel controller in context.Result.Controllers)
        {
            foreach (ActionModel action in controller.Actions)
            {
                var returnType = action.ActionMethod.ReturnType;
                if (returnType.IsGenericType && returnType.GenericTypeArguments[0].IsGenericType)
                {
                    var genericType = returnType.GenericTypeArguments[0].GetGenericArguments()[0];
                    action.Filters.Add(new ProducesResponseTypeAttribute(genericType, StatusCodes.Status200OK));
                }
            }
        }
    }
}