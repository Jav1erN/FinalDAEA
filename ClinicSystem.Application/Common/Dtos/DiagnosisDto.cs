namespace ClinicSystem.Application.Common.Dtos;

public class DiagnosisDto
{
    public Guid DiagnosisId { get; set; } = Guid.Empty;

    public Guid MedicalRecordId { get; set; } = Guid.Empty;

    public string Cie10Code { get; set; } = string.Empty;

    public string? Description { get; set; } = null;

    public bool? IsPrimary { get; set; } = null;

    public DateTime? NotedAt { get; set; } = null;
}

