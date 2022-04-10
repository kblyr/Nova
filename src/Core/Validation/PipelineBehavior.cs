using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Nova.Validation;

sealed class PipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, IResponse> where TRequest : IRequest
{
    readonly IEnumerable<IValidator<TRequest>> _validators;

    public PipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<IResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<IResponse> next)
    {
        var failures = new List<ValidationFailure>();

        foreach (var validator in _validators)
        {
            var result = await validator.ValidateAsync(request);

            if (result.Errors is { Count: > 0 })
                failures.AddRange(result.Errors);
        }

        if (failures.Any())
            return new ValidationFailedResponse(failures.Select(failure => new ValidationFailedResponse.FailureObj(failure.PropertyName, failure.ErrorMessage)));

        return await next();
    }
}