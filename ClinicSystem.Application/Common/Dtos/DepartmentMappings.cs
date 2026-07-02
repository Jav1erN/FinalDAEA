using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Common.Dtos;

public static class DepartmentMappings
{
    public static DepartmentDto ToDto(this Department entity)
    {
        return new DepartmentDto
        {
            DepartmentId = entity.DepartmentId,
            Name = entity.Name,
            Description = entity.Description,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}

