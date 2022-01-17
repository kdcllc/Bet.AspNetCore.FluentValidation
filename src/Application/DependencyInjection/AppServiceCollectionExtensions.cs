using Application.TodoCommands;
using Application.Validators;

using Bet.AspNetCore.FluentValidation.Behaviors;

using FluentValidation;

using MediatR;

namespace Microsoft.Extensions.DependencyInjection;

public static class AppServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // add database
        services.AddDatabase();

        // add automapper support
        services.AddAutoMapper(typeof(AppServiceCollectionExtensions));

        // add mediar support
        services.AddMediatR(typeof(AppServiceCollectionExtensions));

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorPipelineBehavior<,>));

        services.AddTransient<IValidator<CreateCommand>, CreateCommandValidator>();

        return services;
    }

}
