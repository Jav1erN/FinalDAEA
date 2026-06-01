using ClinicSystem.Application.UseCases.RefreshTokens.Commands;
using ClinicSystem.Application.UseCases.RefreshTokens.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.API.Controllers;

[ApiController]
[Route("api/refresh-tokens")]
public class RefreshTokensController : ControllerBase
{
    private readonly ISender _sender;

    public RefreshTokensController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetRefreshTokensQuery(), cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetRefreshTokenByIdQuery(id), cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        if (!EqualityComparer<Guid>.Default.Equals(id, command.RefreshTokenId))
            return BadRequest("Route id does not match request id");

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteRefreshTokenCommand(id), cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return NoContent();
    }
}
