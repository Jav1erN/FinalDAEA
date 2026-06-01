using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.Doctors.Dtos;

public static class DoctorMappings
{
    public static DoctorDto ToDto(this Doctor entity)
    {
        return new DoctorDto
        {
            DoctorId = entity.DoctorId,
            UserId = entity.UserId,
            SpecialtyId = entity.SpecialtyId,
            LicenseNumber = entity.LicenseNumber,
            YearsExperience = entity.YearsExperience,
            ConsultationFee = entity.ConsultationFee,
            Office = entity.Office,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy
        };
    }
}
