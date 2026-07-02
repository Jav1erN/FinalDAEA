using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Common.Dtos;

public static class VitalSignMappings
{
    public static VitalSignDto ToDto(this VitalSign entity)
    {
        return new VitalSignDto
        {
            VitalSignId = entity.VitalSignId,
            MedicalRecordId = entity.MedicalRecordId,
            RecordedBy = entity.RecordedBy,
            SystolicBp = entity.SystolicBp,
            DiastolicBp = entity.DiastolicBp,
            HeartRate = entity.HeartRate,
            Temperature = entity.Temperature,
            RespiratoryRate = entity.RespiratoryRate,
            WeightKg = entity.WeightKg,
            HeightCm = entity.HeightCm,
            Spo2 = entity.Spo2,
            RecordedAt = entity.RecordedAt
        };
    }
}

