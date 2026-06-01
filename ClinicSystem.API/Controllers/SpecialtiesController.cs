using ClinicSystem.Application.UseCases.Specialties.Commands;
using ClinicSystem.Application.UseCases.Specialties.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicSystem.API.Controllers;

[ApiController]
[Route("api/specialtys")]
public class SpecialtiesController : ControllerBase
{
    private readonly ISender _sender;

    public SpecialtiesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetSpecialtiesQuery(), cancellationToken);
        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetSpecialtyByIdQuery(id), cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSpecialtyCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateSpecialtyCommand command, CancellationToken cancellationToken)
    {
        if (!EqualityComparer<Guid>.Default.Equals(id, command.SpecialtyId))
            return BadRequest("Route id does not match request id");

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new DeleteSpecialtyCommand(id), cancellationToken);

        if (result.IsFailure)
            return NotFound(result.Error);

        return NoContent();
    }
}
