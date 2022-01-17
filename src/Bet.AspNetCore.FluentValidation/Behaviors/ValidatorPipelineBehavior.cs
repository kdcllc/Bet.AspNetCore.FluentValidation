
using FluentValidation;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Bet.AspNetCore.FluentValidation.Behaviors;

public class ValidatorPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ValidatorPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly List<IValidator<TRequest>> _validators; // = new List<IValidator<TRequest>>();

    public ValidatorPipelineBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidatorPipelineBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators.ToList();
        _logger = logger;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (request == null)
        {
            return next();
        }

        var typeName = request.GetGenericTypeName();

        _logger.LogDebug("----- Validating command {CommandType}", typeName);

        var failures = _validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            _logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, request, failures);

            // Map the validation failures and throw an error,
            // this stops the execution of the request
            var errors = failures
                .GroupBy(x => x.PropertyName)
                .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage).ToArray());

            throw new InputValidationException($"Command Validation Errors for type {typeof(TRequest).Name}", errors);
        }

        return next();
    }
}