
using MediatR;

namespace Application.TodoQueries;

public record GetTodoQuery(int TodoId) : IRequest<TodoQueryItem>;
