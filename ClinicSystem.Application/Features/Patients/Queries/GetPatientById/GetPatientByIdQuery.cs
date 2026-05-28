using MediatR;
using ClinicSystem.Application.Common.Models;

namespace ClinicSystem.Application.Features.Patients.Queries.GetPatientById;

public record GetPatientByIdQuery(Guid PatientId)
    : IRequest<Result<PatientResponse>>
{
    public class Handler : IRequestHandler<GetPatientByIdQuery, Result<PatientResponse>>
    {
        public async Task<Result<PatientResponse>> Handle(
            GetPatientByIdQuery request,
            CancellationToken cancellationToken)
        {
            // ⚠️ luego con DbContext real
            return Result<PatientResponse>.Success(new PatientResponse
            {
                PatientId = request.PatientId,
                FullName = "Paciente Demo"
            });
        }
    }
}