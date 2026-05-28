using MediatR;
using ClinicSystem.Application.Common.Models;

namespace ClinicSystem.Application.Features.Patients.Commands.CreatePatient;

public record CreatePatientCommand(
    string FirstName,
    string LastName,
    string Email
) : IRequest<Result<CreatePatientResponse>>
{
    public class Handler : IRequestHandler<CreatePatientCommand, Result<CreatePatientResponse>>
    {
        public async Task<Result<CreatePatientResponse>> Handle(
            CreatePatientCommand request,
            CancellationToken cancellationToken)
        {
            // ⚠️ lógica real luego con DbContext
            return Result<CreatePatientResponse>.Success(new CreatePatientResponse
            {
                PatientId = Guid.NewGuid()
            });
        }
    }
}