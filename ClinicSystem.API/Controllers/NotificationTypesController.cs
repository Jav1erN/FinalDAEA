using ClinicSystem.Application.UseCases.NotificationTypes.Commands;
using ClinicSystem.Application.UseCases.NotificationTypes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.API.Controllers;

[ApiController]
[Route("api/notification-types")]
public class NotificationTypesController : ControllerBase
{
    private readonly ISender _sender;

    public NotificationTypesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetNotificationTypesQuery(), cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetNotificationTypeByIdQuery(id), cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateNotificationTypeCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateNotificationTypeCommand command, CancellationToken cancellationToken)
    {
        if (!EqualityComparer<Guid>.Default.Equals(id, command.TypeId))
            return BadRequest("Route id does not match request id");

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteNotificationTypeCommand(id), cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return NoContent();
    }
}
