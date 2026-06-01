using MediatR;
using ClinicSystem.Application.Common.Models;
using ClinicSystem.Infrastructure.Persistence.Context;
using ClinicSystem.Infrastructure.Persistence.Entities;

namespace ClinicSystem.Application.Features.Patients.Commands.CreatePatient;

public record CreatePatientCommand(
    string DocumentNumber,
    string FirstName,
    string LastName,
    string Email
) : IRequest<Result<CreatePatientResponse>>
{
    public class Handler : IRequestHandler<CreatePatientCommand, Result<CreatePatientResponse>>
    {
        private readonly ClinicDbContext _context;

        public Handler(ClinicDbContext context)
        {
            _context = context;
        }

        public async Task<Result<CreatePatientResponse>> Handle(
            CreatePatientCommand request,
            CancellationToken cancellationToken)
        {
            var patient = new patient
            {
                patient_id = Guid.NewGuid(),
                document_number = request.DocumentNumber,
                first_name = request.FirstName,
                last_name = request.LastName,
                email = request.Email,
                created_at = DateTime.UtcNow
            };

            _context.patients.Add(patient);

            await _context.SaveChangesAsync(cancellationToken);

            return Result<CreatePatientResponse>.Success(new CreatePatientResponse
            {
                PatientId = patient.patient_id
            });
        }
    }
}