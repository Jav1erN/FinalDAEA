using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.Diagnoses.Dtos;

public static class DiagnosisMappings
{
    public static DiagnosisDto ToDto(this Diagnosis entity)
    {
        return new DiagnosisDto
        {
            DiagnosisId = entity.DiagnosisId,
            MedicalRecordId = entity.MedicalRecordId,
            Cie10Code = entity.Cie10Code,
            Description = entity.Description,
            IsPrimary = entity.IsPrimary,
            NotedAt = entity.NotedAt
        };
    }
}
