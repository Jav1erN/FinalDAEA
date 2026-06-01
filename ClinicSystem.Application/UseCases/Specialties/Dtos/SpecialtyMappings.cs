using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.Specialties.Dtos;

public static class SpecialtyMappings
{
    public static SpecialtyDto ToDto(this Specialty entity)
    {
        return new SpecialtyDto
        {
            SpecialtyId = entity.SpecialtyId,
            DepartmentId = entity.DepartmentId,
            Name = entity.Name,
            Description = entity.Description,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
