using ClinicSystem.Domain.Entities;

namespace ClinicSystem.Application.Common.Dtos;

public static class RefreshTokenMappings
{
    public static RefreshTokenDto ToDto(this RefreshToken entity)
    {
        return new RefreshTokenDto
        {
            RefreshTokenId = entity.RefreshTokenId,
            UserId = entity.UserId,
            TokenHash = entity.TokenHash,
            ExpiresAt = entity.ExpiresAt,
            RevokedAt = entity.RevokedAt,
            CreatedAt = entity.CreatedAt
        };
    }
}

