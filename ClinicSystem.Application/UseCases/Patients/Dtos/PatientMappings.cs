using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.Patients.Dtos;

public static class PatientMappings
{
    public static PatientDto ToDto(this Patient entity)
    {
        return new PatientDto
        {
            PatientId = entity.PatientId,
            UserId = entity.UserId,
            DocumentNumber = entity.DocumentNumber,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            BirthDate = entity.BirthDate,
            Gender = entity.Gender,
            BloodType = entity.BloodType,
            Phone = entity.Phone,
            Email = entity.Email,
            Address = entity.Address,
            EmergencyContactName = entity.EmergencyContactName,
            EmergencyContactPhone = entity.EmergencyContactPhone,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy
        };
    }
}

