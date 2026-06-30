using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.UseCases.Users.Dtos;

public static class UserMappings
{
    public static UserDto ToDto(this User entity)
    {
        return new UserDto
        {
            UserId = entity.UserId,
            RoleId = entity.RoleId,
            Username = entity.Username,
            Email = entity.Email,
            PasswordHash = entity.PasswordHash,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Phone = entity.Phone,
            IsActive = entity.IsActive,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            DeletedAt = entity.DeletedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy
        };
    }
}

