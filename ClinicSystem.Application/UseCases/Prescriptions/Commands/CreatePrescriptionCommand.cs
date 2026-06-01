using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.Prescriptions.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.Prescriptions.Commands;

public record CreatePrescriptionCommand(
    Guid MedicalRecordId,
    Guid DoctorId,
    Guid PatientId,
    DateOnly? ValidUntil,
    DateTime? DispensedAt,
    Guid? DispensedBy,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<PrescriptionDto>>;

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

        await _unitOfWork.Repository<Prescription>().AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<PrescriptionDto>.Success(entity.ToDto());
    }
}
