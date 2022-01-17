using MediatR;

namespace Application.TodoQueries;

public class GetTodoQueryHandler : IRequestHandler<GetTodoQuery, TodoQueryItem>
{
    public Task<TodoQueryItem> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
