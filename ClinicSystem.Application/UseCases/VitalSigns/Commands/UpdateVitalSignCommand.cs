using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.VitalSigns.Commands;

public class UpdateVitalSignCommand : IRequest<Result<VitalSignDto>>
{
    public Guid VitalSignId { get; set; } = Guid.Empty;

    public Guid MedicalRecordId { get; set; } = Guid.Empty;

    public Guid? RecordedBy { get; set; }

    public int? SystolicBp { get; set; }

    public int? DiastolicBp { get; set; }

    public int? HeartRate { get; set; }

    public decimal? Temperature { get; set; }

    public int? RespiratoryRate { get; set; }

    public decimal? WeightKg { get; set; }

    public decimal? HeightCm { get; set; }

    public int? Spo2 { get; set; }

    public DateTime? RecordedAt { get; set; }
}

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
        var repository = _unitOfWork.VitalSigns;
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

