using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Nova.Validation;

sealed class ValidationPipelineBehavior<TRequest> : IPipelineBehavior<TRequest, Response> where TRequest : Request
{
    readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<Response> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Response> next)
    {
        var failures = new List<ValidationFailure>();

        if (_validators == null || !_validators.Any())
            return await next();

        foreach (var validator in _validators)
        {
            var result = await validator.ValidateAsync(request);

            if (result.Errors is { Count: > 0 })
                failures.AddRange(result.Errors);
        }

        if (failures.Count > 0)
        {
            return new ValidationFailed(failures.Select(failure => new ValidationFailed.FailureObj(failure.PropertyName, failure.ErrorMessage)));
        }

        return await next();
    }
}