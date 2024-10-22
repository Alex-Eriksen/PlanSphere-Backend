using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace PlanSphere.Core.Validation;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators ?? throw new ArgumentNullException(nameof(validators));

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var validationResults = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken)));
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
        if (failures.Exists(f => f.ErrorCode == StatusCodes.Status401Unauthorized.ToString()))
        {
            var unauthorizedFailure = failures.First(f => f.ErrorCode == StatusCodes.Status401Unauthorized.ToString());
            throw new UnauthorizedAccessException(unauthorizedFailure.ErrorMessage);
        }
        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}