using MediatR;
using Microsoft.AspNetCore.Mvc;
using ClinicSystem.Application.Features.Patients.Commands.CreatePatient;

namespace ClinicSystem.API.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly ISender _sender;

    public PatientsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePatientCommand command)
    {
        var result = await _sender.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}