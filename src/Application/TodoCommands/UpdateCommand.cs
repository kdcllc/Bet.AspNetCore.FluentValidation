using MediatR;

namespace Application.TodoCommands;

public record UpdateCommand(TodoItemDto Item) : IRequest<UpdateResult>;