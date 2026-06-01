namespace ClinicSystem.Application.UseCases.PrescriptionDetails.Dtos;

public class PrescriptionDetailDto
{
    public Guid PrescriptionDetailId { get; set; } = Guid.Empty;

    public Guid PrescriptionId { get; set; } = Guid.Empty;

    public Guid MedicationId { get; set; } = Guid.Empty;

    public string? Dosage { get; set; } = null;

    public string? Frequency { get; set; } = null;

    public int? DurationDays { get; set; } = null;

    public int QuantityPrescribed { get; set; } = 0;

    public string? Instructions { get; set; } = null;

    public bool? IsSubstitutable { get; set; } = null;
}
