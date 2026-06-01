using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.Medications.Dtos;

public static class MedicationMappings
{
    public static MedicationDto ToDto(this Medication entity)
    {
        return new MedicationDto
        {
            MedicationId = entity.MedicationId,
            Name = entity.Name,
            GenericName = entity.GenericName,
            Presentation = entity.Presentation,
            Concentration = entity.Concentration,
            Laboratory = entity.Laboratory,
            RequiresPrescription = entity.RequiresPrescription,
            Stock = entity.Stock,
            UnitPrice = entity.UnitPrice,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy
        };
    }
}
