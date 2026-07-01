using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Prescriptions.Commands;

public class CreatePrescriptionCommand : IRequest<Result<PrescriptionDto>>
{
    public Guid MedicalRecordId { get; set; } = Guid.Empty;

    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public DateOnly? ValidUntil { get; set; }

    public DateTime? DispensedAt { get; set; }

    public Guid? DispensedBy { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

public class CreatePrescriptionCommandHandler
    : IRequestHandler<CreatePrescriptionCommand, Result<PrescriptionDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePrescriptionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PrescriptionDto>> Handle(
        CreatePrescriptionCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Prescription
        {
            PrescriptionId = Guid.NewGuid(),
            MedicalRecordId = request.MedicalRecordId,
            DoctorId = request.DoctorId,
            PatientId = request.PatientId,
            ValidUntil = request.ValidUntil,
            DispensedAt = request.DispensedAt,
            DispensedBy = request.DispensedBy,
            CreatedBy = request.CreatedBy,
            UpdatedBy = request.UpdatedBy
        };

        await _unitOfWork.Prescriptions.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PrescriptionDto>.Success(entity.ToDto());
    }
}

