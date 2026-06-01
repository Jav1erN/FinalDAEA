using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.PrescriptionDetails.Dtos;

public static class PrescriptionDetailMappings
{
    public static PrescriptionDetailDto ToDto(this PrescriptionDetail entity)
    {
        return new PrescriptionDetailDto
        {
            PrescriptionDetailId = entity.PrescriptionDetailId,
            PrescriptionId = entity.PrescriptionId,
            MedicationId = entity.MedicationId,
            Dosage = entity.Dosage,
            Frequency = entity.Frequency,
            DurationDays = entity.DurationDays,
            QuantityPrescribed = entity.QuantityPrescribed,
            Instructions = entity.Instructions,
            IsSubstitutable = entity.IsSubstitutable
        };
    }
}
