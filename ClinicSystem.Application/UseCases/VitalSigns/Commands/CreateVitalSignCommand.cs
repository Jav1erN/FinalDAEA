using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.UseCases.VitalSigns.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.VitalSigns.Commands;

public record CreateVitalSignCommand(
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

public class CreateVitalSignCommandHandler
    : IRequestHandler<CreateVitalSignCommand, Result<VitalSignDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateVitalSignCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<VitalSignDto>> Handle(
        CreateVitalSignCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new VitalSign
        {
            VitalSignId = Guid.NewGuid(),
            MedicalRecordId = request.MedicalRecordId,
            RecordedBy = request.RecordedBy,
            SystolicBp = request.SystolicBp,
            DiastolicBp = request.DiastolicBp,
            HeartRate = request.HeartRate,
            Temperature = request.Temperature,
            RespiratoryRate = request.RespiratoryRate,
            WeightKg = request.WeightKg,
            HeightCm = request.HeightCm,
            Spo2 = request.Spo2,
            RecordedAt = request.RecordedAt
        };

        await _unitOfWork.VitalSigns.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<VitalSignDto>.Success(entity.ToDto());
    }
}

