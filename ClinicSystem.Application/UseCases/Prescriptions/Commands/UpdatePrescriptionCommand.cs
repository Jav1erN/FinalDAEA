using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Prescriptions.Commands;

public class UpdatePrescriptionCommand : IRequest<Result<PrescriptionDto>>
{
    public Guid PrescriptionId { get; set; } = Guid.Empty;

    public Guid MedicalRecordId { get; set; } = Guid.Empty;

    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public DateOnly? ValidUntil { get; set; }

    public DateTime? DispensedAt { get; set; }

    public Guid? DispensedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

public class UpdatePrescriptionCommandHandler
    : IRequestHandler<UpdatePrescriptionCommand, Result<PrescriptionDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePrescriptionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PrescriptionDto>> Handle(
        UpdatePrescriptionCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Prescriptions;
        var entity = await repository.GetByIdAsync(request.PrescriptionId, cancellationToken);

        if (entity is null)
            return Result<PrescriptionDto>.Failure("Prescription not found");

        entity.MedicalRecordId = request.MedicalRecordId;
        entity.DoctorId = request.DoctorId;
        entity.PatientId = request.PatientId;
        entity.ValidUntil = request.ValidUntil;
        entity.DispensedAt = request.DispensedAt;
        entity.DispensedBy = request.DispensedBy;
        entity.UpdatedAt = request.UpdatedAt;
        entity.CreatedBy = request.CreatedBy;
        entity.UpdatedBy = request.UpdatedBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PrescriptionDto>.Success(entity.ToDto());
    }
}

