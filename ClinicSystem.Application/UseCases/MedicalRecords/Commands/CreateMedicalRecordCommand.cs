using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.MedicalRecords.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.MedicalRecords.Commands;

public record CreateMedicalRecordCommand(
    Guid PatientId,
    Guid DoctorId,
    Guid? AppointmentId,
    string? ChiefComplaint,
    string? Diagnosis,
    string? Treatment,
    string? Observations,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<MedicalRecordDto>>;

public class CreateMedicalRecordCommandHandler
    : IRequestHandler<CreateMedicalRecordCommand, Result<MedicalRecordDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateMedicalRecordCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MedicalRecordDto>> Handle(
        CreateMedicalRecordCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new MedicalRecord
        {
            MedicalRecordId = Guid.NewGuid(),
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            AppointmentId = request.AppointmentId,
            ChiefComplaint = request.ChiefComplaint,
            Diagnosis = request.Diagnosis,
            Treatment = request.Treatment,
            Observations = request.Observations,
            CreatedBy = request.CreatedBy,
            UpdatedBy = request.UpdatedBy
        };

        await _unitOfWork.Repository<MedicalRecord>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<MedicalRecordDto>.Success(entity.ToDto());
    }
}
