using MediatR;
using ClinicSystem.Application.Common.Models;
using ClinicSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ClinicSystem.Application.Features.Patients.Queries.GetPatientById;

public record GetPatientByIdQuery(Guid PatientId)
    : IRequest<Result<PatientResponse>>
{
    public class Handler : IRequestHandler<GetPatientByIdQuery, Result<PatientResponse>>
    {
        private readonly ClinicDbContext _context;

        public Handler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<Result<PatientResponse>> Handle(
            GetPatientByIdQuery request,
            CancellationToken cancellationToken)
        {
            var patient = await _context.patients
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.patient_id == request.PatientId,
                    cancellationToken);

            if (patient is null)
                return Result<PatientResponse>.Failure("Patient not found");

            return Result<PatientResponse>.Success(new PatientResponse
            {
                PatientId = patient.patient_id,
                FullName = $"{patient.first_name} {patient.last_name}"
            });
        }
    }
}