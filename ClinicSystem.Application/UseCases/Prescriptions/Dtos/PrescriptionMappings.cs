using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.Prescriptions.Dtos;

public static class PrescriptionMappings
{
    public static PrescriptionDto ToDto(this Prescription entity)
    {
        return new PrescriptionDto
        {
            PrescriptionId = entity.PrescriptionId,
            MedicalRecordId = entity.MedicalRecordId,
            DoctorId = entity.DoctorId,
            PatientId = entity.PatientId,
            ValidUntil = entity.ValidUntil,
            DispensedAt = entity.DispensedAt,
            DispensedBy = entity.DispensedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy
        };
    }
}

