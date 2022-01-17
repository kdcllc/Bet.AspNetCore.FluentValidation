using AutoMapper;

using Domain;

using Infastructure.Data;

using MediatR;

namespace Application.TodoCommands;

public class CreateCommandHandler : IRequestHandler<CreateCommand, CreateResult>
{
    private readonly IMapper mapper;
    private readonly IAsyncRepository<TodoItem> repository;

    public CreateCommandHandler(IMapper mapper, IAsyncRepository<TodoItem> repository)
    {
        this.mapper=mapper;
        this.repository=repository;
    }

    public async Task<CreateResult> Handle(CreateCommand request, CancellationToken cancellationToken)
    {
        var model = mapper.Map<TodoItem>(request);

        var result = await repository.AddAsync(model, cancellationToken);

        return new CreateResult(result.Id);
    }
}
