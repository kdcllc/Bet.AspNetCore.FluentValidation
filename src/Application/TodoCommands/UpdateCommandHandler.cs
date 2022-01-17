using MediatR;

namespace Application.TodoCommands;

public class UpdateCommandHandler : IRequestHandler<UpdateCommand, UpdateResult>
{
    public Task<UpdateResult> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
