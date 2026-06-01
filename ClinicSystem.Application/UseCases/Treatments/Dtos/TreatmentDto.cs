namespace ClinicSystem.Application.UseCases.Treatments.Dtos;

public class TreatmentDto
{
    public Guid TreatmentId { get; set; } = Guid.Empty;

    public Guid MedicalRecordId { get; set; } = Guid.Empty;

    public string Description { get; set; } = string.Empty;

    public DateOnly? StartDate { get; set; } = null;

    public DateOnly? EndDate { get; set; } = null;

    public string? Status { get; set; } = null;

    public string? Notes { get; set; } = null;
}
