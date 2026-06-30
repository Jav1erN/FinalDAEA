using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.MedicalRecords.Dtos;

public static class MedicalRecordMappings
{
    public static MedicalRecordDto ToDto(this MedicalRecord entity)
    {
        return new MedicalRecordDto
        {
            MedicalRecordId = entity.MedicalRecordId,
            PatientId = entity.PatientId,
            DoctorId = entity.DoctorId,
            AppointmentId = entity.AppointmentId,
            ChiefComplaint = entity.ChiefComplaint,
            Diagnosis = entity.Diagnosis,
            Treatment = entity.Treatment,
            Observations = entity.Observations,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy
        };
    }
}

