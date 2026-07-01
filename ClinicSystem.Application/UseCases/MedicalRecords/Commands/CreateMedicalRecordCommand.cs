using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.MedicalRecords.Commands;

public class CreateMedicalRecordCommand : IRequest<Result<MedicalRecordDto>>
{
    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid? AppointmentId { get; set; }

    public string? ChiefComplaint { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public string? Observations { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

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

        await _unitOfWork.MedicalRecords.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<MedicalRecordDto>.Success(entity.ToDto());
    }
}

