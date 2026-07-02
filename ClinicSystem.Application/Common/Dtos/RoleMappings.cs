using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Common.Dtos;

public static class RoleMappings
{
    public static RoleDto ToDto(this Role entity)
    {
        return new RoleDto
        {
            RoleId = entity.RoleId,
            Name = entity.Name,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}

