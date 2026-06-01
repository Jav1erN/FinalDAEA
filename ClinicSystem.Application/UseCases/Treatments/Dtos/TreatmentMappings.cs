using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.Treatments.Dtos;

public static class TreatmentMappings
{
    public static TreatmentDto ToDto(this Treatment entity)
    {
        return new TreatmentDto
        {
            TreatmentId = entity.TreatmentId,
            MedicalRecordId = entity.MedicalRecordId,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Status = entity.Status,
            Notes = entity.Notes
        };
    }
}
