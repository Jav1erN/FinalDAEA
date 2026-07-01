using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.MedicalRecords.Commands;

public class UpdateMedicalRecordCommand : IRequest<Result<MedicalRecordDto>>
{
    public Guid MedicalRecordId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid? AppointmentId { get; set; }

    public string? ChiefComplaint { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public string? Observations { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }
}

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
        var repository = _unitOfWork.MedicalRecords;
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

