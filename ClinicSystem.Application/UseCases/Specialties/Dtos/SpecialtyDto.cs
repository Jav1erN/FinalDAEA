namespace ClinicSystem.Application.UseCases.Specialties.Dtos;

public class SpecialtyDto
{
    public Guid SpecialtyId { get; set; } = Guid.Empty;

    public Guid DepartmentId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = null;

    public bool? IsActive { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;
}
