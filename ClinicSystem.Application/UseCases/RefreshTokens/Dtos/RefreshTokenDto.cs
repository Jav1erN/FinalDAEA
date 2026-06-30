namespace ClinicSystem.Application.UseCases.RefreshTokens.Dtos;

public class RefreshTokenDto
{
    public Guid RefreshTokenId { get; set; } = Guid.Empty;

    public Guid UserId { get; set; } = Guid.Empty;

    public string TokenHash { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow;

    public DateTime? RevokedAt { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;
}

