using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Nova.Validation;

sealed class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Response> 
    where TRequest : IRequest<Response>
{
    readonly IEnumerable<IValidator<TRequest>> _validators;
    readonly IEnumerable<IAccessValidationConfiguration<TRequest>> _accessValidationConfigurations;
    readonly IAccessValidator _accessValidator;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators, IEnumerable<IAccessValidationConfiguration<TRequest>> accessValidationConfigurations, IAccessValidator accessValidator)
    {
        _validators = validators;
        _accessValidationConfigurations = accessValidationConfigurations;
        _accessValidator = accessValidator;
    }

    public async Task<Response> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<Response> next)
    {
        var failures = await Validate(_validators, request);

        if (failures.Any())
            return new ValidationFailed(failures.Select(failure => new ValidationFailed.FailureObj(failure.PropertyName, failure.ErrorMessage)));

        foreach (var configuration in _accessValidationConfigurations)
        {
            if (Validate(_accessValidator, configuration, request) == false)
                return new AccessValidationFailed();
        }

        return await next();
    }

    static async Task<IEnumerable<ValidationFailure>> Validate(IEnumerable<IValidator<TRequest>> validators, TRequest request)
    {
        if (validators == null || !validators.Any())
            return Enumerable.Empty<ValidationFailure>();

        var failures = new List<ValidationFailure>();

        foreach (var validator in validators)
        {
            var result = await validator.ValidateAsync(request);

            if (result.Errors is { Count: > 0 })
                failures.AddRange(result.Errors);
        }

        return failures;
    }

    static bool Validate(IAccessValidator validator, IAccessValidationConfiguration<TRequest> configuration, TRequest request)
    {
        var context = new AccessValidationContext<TRequest>(request);
        configuration.Configure(context);
        var result = validator.Validate(context.Rules);
        return result.IsSucceeded;
    }
}