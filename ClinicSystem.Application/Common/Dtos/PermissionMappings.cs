using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Common.Dtos;

public static class PermissionMappings
{
    public static PermissionDto ToDto(this Permission entity)
    {
        return new PermissionDto
        {
            PermissionId = entity.PermissionId,
            Resource = entity.Resource,
            Action = entity.Action,
            Description = entity.Description,
            CreatedAt = entity.CreatedAt
        };
    }
}

