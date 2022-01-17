using MediatR;

namespace Application.TodoCommands;

public record CreateCommand(TodoItemDto Item) : IRequest<CreateResult>;
