namespace ClinicSystem.Application.UseCases.Roles.Dtos;

public class RoleDto
{
    public Guid RoleId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;
}

