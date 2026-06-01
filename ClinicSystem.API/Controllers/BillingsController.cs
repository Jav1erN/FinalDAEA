using ClinicSystem.Application.UseCases.Billings.Commands;
using ClinicSystem.Application.UseCases.Billings.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.API.Controllers;

[ApiController]
[Route("api/billings")]
public class BillingsController : ControllerBase
{
    private readonly ISender _sender;

    public BillingsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetBillingsQuery(), cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetBillingByIdQuery(id), cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBillingCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateBillingCommand command, CancellationToken cancellationToken)
    {
        if (!EqualityComparer<Guid>.Default.Equals(id, command.BillingId))
            return BadRequest("Route id does not match request id");

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteBillingCommand(id), cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return NoContent();
    }
}
