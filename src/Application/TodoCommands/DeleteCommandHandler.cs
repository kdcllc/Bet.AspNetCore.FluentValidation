using MediatR;

namespace Application.TodoCommands
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand, bool>
    {
        public Task<bool> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
