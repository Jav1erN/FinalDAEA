using ClinicSystem.Application.UseCases.BillingDetails.Commands;
using ClinicSystem.Application.UseCases.BillingDetails.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.API.Controllers;

[ApiController]
[Route("api/billing-details")]
public class BillingDetailsController : ControllerBase
{
    private readonly ISender _sender;

    public BillingDetailsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetBillingDetailsQuery(), cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetBillingDetailByIdQuery(id), cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBillingDetailCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateBillingDetailCommand command, CancellationToken cancellationToken)
    {
        if (!EqualityComparer<Guid>.Default.Equals(id, command.BillingDetailId))
            return BadRequest("Route id does not match request id");

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteBillingDetailCommand(id), cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return NoContent();
    }
}
