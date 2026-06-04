using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Prescriptions.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Repositories;
using MediatR;

namespace ClinicSystem.Application.UseCases.Prescriptions.Commands;

public record UpdatePrescriptionCommand(
    Guid PrescriptionId,
    Guid MedicalRecordId,
    Guid DoctorId,
    Guid PatientId,
    DateOnly? ValidUntil,
    DateTime? DispensedAt,
    Guid? DispensedBy,
    DateTime? UpdatedAt,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<PrescriptionDto>>;

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
        var repository = _unitOfWork.Repository<Prescription>();
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
