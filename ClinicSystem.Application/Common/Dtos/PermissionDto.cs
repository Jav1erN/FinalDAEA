namespace ClinicSystem.Application.Common.Dtos;

public class PermissionDto
{
    public Guid PermissionId { get; set; } = Guid.Empty;

    public string Resource { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string? Description { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;
}

