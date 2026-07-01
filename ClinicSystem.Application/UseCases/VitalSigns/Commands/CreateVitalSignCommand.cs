using ClinicSystem.Application.Common.Models;
using ClinicSystem.Application.Common.Dtos;
using ClinicSystem.Domain.Entities;
using ClinicSystem.Domain.Ports.Persistence;
using MediatR;

namespace ClinicSystem.Application.UseCases.VitalSigns.Commands;

public class CreateVitalSignCommand : IRequest<Result<VitalSignDto>>
{
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

