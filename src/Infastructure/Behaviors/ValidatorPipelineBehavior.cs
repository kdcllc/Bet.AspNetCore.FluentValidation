using FluentValidation;
using MediatR;

namespace Infastructure.Behaviors;

public class ValidatorPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
      => _validators = validators;

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        // Invoke the validators
        var failures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .ToArray();

        if (failures.Length > 0)
        {
            throw new ValidationException("Validation exception", failures);
        }

        // Invoke the next handler
        // (can be another pipeline behavior or the request handler)
        return next();
    }
}