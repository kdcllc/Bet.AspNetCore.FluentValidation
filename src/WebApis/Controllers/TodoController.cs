using Application.TodoCommands;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace WebApis.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TodoController : ControllerBase
{
    private readonly IMediator mediator;

    public TodoController(IMediator mediator)
    {
        this.mediator=mediator;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<CreateResult>> CreateAsync(CreateCommand createCommand, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(createCommand, cancellationToken);

        return Ok(result);
    }
}
