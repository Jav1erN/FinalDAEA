namespace ClinicSystem.Application.Common.Dtos;

public class LaboratoryTestDto
{
    public Guid LaboratoryTestId { get; set; } = Guid.Empty;

    public Guid PatientId { get; set; } = Guid.Empty;

    public Guid DoctorId { get; set; } = Guid.Empty;

    public Guid? MedicalRecordId { get; set; } = null;

    public string TestName { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime? RequestedDate { get; set; } = null;

    public DateTime? SampleTakenDate { get; set; } = null;

    public DateTime? CompletedDate { get; set; } = null;

    public string? Observations { get; set; } = null;

    public DateTime? CreatedAt { get; set; } = null;

    public DateTime? UpdatedAt { get; set; } = null;

    public DateTime? DeletedAt { get; set; } = null;

    public Guid? CreatedBy { get; set; } = null;

    public Guid? UpdatedBy { get; set; } = null;
}

