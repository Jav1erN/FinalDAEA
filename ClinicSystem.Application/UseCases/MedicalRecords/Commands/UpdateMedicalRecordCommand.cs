using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.MedicalRecords.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.MedicalRecords.Commands;

public record UpdateMedicalRecordCommand(
    Guid MedicalRecordId,
    Guid PatientId,
    Guid DoctorId,
    Guid? AppointmentId,
    string? ChiefComplaint,
    string? Diagnosis,
    string? Treatment,
    string? Observations,
    DateTime? UpdatedAt,
    Guid? CreatedBy,
    Guid? UpdatedBy
) : IRequest<Result<MedicalRecordDto>>;

public class UpdateMedicalRecordCommandHandler
    : IRequestHandler<UpdateMedicalRecordCommand, Result<MedicalRecordDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMedicalRecordCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MedicalRecordDto>> Handle(
        UpdateMedicalRecordCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<MedicalRecord>();
        var entity = await repository.GetByIdAsync(request.MedicalRecordId, cancellationToken);

        if (entity is null)
            return Result<MedicalRecordDto>.Failure("MedicalRecord not found");

        entity.PatientId = request.PatientId;
        entity.DoctorId = request.DoctorId;
        entity.AppointmentId = request.AppointmentId;
        entity.ChiefComplaint = request.ChiefComplaint;
        entity.Diagnosis = request.Diagnosis;
        entity.Treatment = request.Treatment;
        entity.Observations = request.Observations;
        entity.UpdatedAt = request.UpdatedAt;
        entity.CreatedBy = request.CreatedBy;
        entity.UpdatedBy = request.UpdatedBy;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<MedicalRecordDto>.Success(entity.ToDto());
    }
}
