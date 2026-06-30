using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.LaboratoryTests.Dtos;

public static class LaboratoryTestMappings
{
    public static LaboratoryTestDto ToDto(this LaboratoryTest entity)
    {
        return new LaboratoryTestDto
        {
            LaboratoryTestId = entity.LaboratoryTestId,
            PatientId = entity.PatientId,
            DoctorId = entity.DoctorId,
            MedicalRecordId = entity.MedicalRecordId,
            TestName = entity.TestName,
            Status = entity.Status,
            RequestedDate = entity.RequestedDate,
            SampleTakenDate = entity.SampleTakenDate,
            CompletedDate = entity.CompletedDate,
            Observations = entity.Observations,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy
        };
    }
}

