using Application.TodoCommands;

using FluentValidation;

namespace Application.Validators;

public class CreateCommandValidator : AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(c => c.Item.Title).NotEmpty();

        RuleFor(c => c.Item.UserId).GreaterThan(0).WithMessage("Provide UserId.");
    }
}
