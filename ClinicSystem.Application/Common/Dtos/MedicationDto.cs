namespace ClinicSystem.Application.Common.Dtos;

public class MedicationDto
{
    public Guid MedicationId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public string? GenericName { get; set; } = null;

    public string? Presentation { get; set; } = null;

    public string? Concentration { get; set; } = null;

    public string? Laboratory { get; set; } = null;

    public bool? RequiresPrescription { get; set; } = null;

    public int? Stock { get; set; } = null;

    public decimal? UnitPrice { get; set; } = null;

    public bool? IsActive { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;

    public DateTime? DeletedAt { get; set; } = null;

    public Guid? CreatedBy { get; set; } = null;

    public Guid? UpdatedBy { get; set; } = null;
}

