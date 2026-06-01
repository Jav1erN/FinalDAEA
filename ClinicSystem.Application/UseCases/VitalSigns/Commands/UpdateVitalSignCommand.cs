using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Ports.Persistence;
using ClinicSystem.Application.UseCases.VitalSigns.Dtos;
using ClinicSystem.Domain.Entities;
using MediatR;

namespace ClinicSystem.Application.UseCases.VitalSigns.Commands;

public record UpdateVitalSignCommand(
    Guid VitalSignId,
    Guid MedicalRecordId,
    Guid? RecordedBy,
    int? SystolicBp,
    int? DiastolicBp,
    int? HeartRate,
    decimal? Temperature,
    int? RespiratoryRate,
    decimal? WeightKg,
    decimal? HeightCm,
    int? Spo2,
    DateTime? RecordedAt
) : IRequest<Result<VitalSignDto>>;

public class UpdateVitalSignCommandHandler
    : IRequestHandler<UpdateVitalSignCommand, Result<VitalSignDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateVitalSignCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<VitalSignDto>> Handle(
        UpdateVitalSignCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<VitalSign>();
        var entity = await repository.GetByIdAsync(request.VitalSignId, cancellationToken);

        if (entity is null)
            return Result<VitalSignDto>.Failure("VitalSign not found");

        entity.MedicalRecordId = request.MedicalRecordId;
        entity.RecordedBy = request.RecordedBy;
        entity.SystolicBp = request.SystolicBp;
        entity.DiastolicBp = request.DiastolicBp;
        entity.HeartRate = request.HeartRate;
        entity.Temperature = request.Temperature;
        entity.RespiratoryRate = request.RespiratoryRate;
        entity.WeightKg = request.WeightKg;
        entity.HeightCm = request.HeightCm;
        entity.Spo2 = request.Spo2;
        entity.RecordedAt = request.RecordedAt;
        repository.Update(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<VitalSignDto>.Success(entity.ToDto());
    }
}
