using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Nova.Validation;

sealed class PipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, IResponse> where TRequest : IRequest
{
    readonly IEnumerable<IValidator<TRequest>> _validators;
    readonly IEnumerable<IAccessValidationConfiguration<TRequest>> _accessValidationConfigurations;
    readonly IAccessValidator _accessValidator;

    public PipelineBehavior(IEnumerable<IValidator<TRequest>> validators, IEnumerable<IAccessValidationConfiguration<TRequest>> accessValidationConfigurations, IAccessValidator accessValidator)
    {
        _validators = validators;
        _accessValidationConfigurations = accessValidationConfigurations;
        _accessValidator = accessValidator;
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

        foreach (var configuration in _accessValidationConfigurations)
        {
            var context = new AccessValidationContext<TRequest>(request);
            configuration.Configure(context);
            var result = _accessValidator.Validate(context.Rules);
            
            if (!result.IsSucceeded)
                return new AccessValidationFailedResponse(result.FailedRules.Select(rule => rule.ErrorMessage));
        }

        return await next();
    }
}