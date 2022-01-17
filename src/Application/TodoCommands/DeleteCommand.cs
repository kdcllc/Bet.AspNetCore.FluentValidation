using MediatR;

namespace Application.TodoCommands;

public record DeleteCommand(int TodoId) : IRequest<bool>;
