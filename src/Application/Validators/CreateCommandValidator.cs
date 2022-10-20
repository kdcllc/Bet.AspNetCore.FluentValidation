using Application.TodoCommands;

using FluentValidation;

using Microsoft.Extensions.Configuration;

namespace Application.Validators;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator(IConfiguration configuration)
    {
        RuleFor(c => c.Item.Title).NotEmpty();

        RuleFor(c => c.Item.UserId).NotEmpty().GreaterThan(0).WithMessage("Provide UserId.");
    }
}
